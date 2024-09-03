using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using formBuilder.Data;
using formBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using formBuilder.ViewModel;
using System.Security.Claims;

namespace formBuilder.Controllers
{
    [Authorize]
    public class FormController : Controller
    {
        private readonly formBuilderContext _context;

        public FormController(formBuilderContext context)
        {
            _context = context;
        }
        public IActionResult SettingsForForms(string searchstring)
        {
            if (!string.IsNullOrEmpty(searchstring))
            {
                return View(_context.Forms.Where(s => s.nameOftheForm.Contains(searchstring)).ToList());
            }
            else
                return View(_context.Forms.ToList());
        }
        public IActionResult FormsPage(string searchstring)
        {
            if (!string.IsNullOrEmpty(searchstring))
            {
                return View(_context.Forms.Where(s => s.nameOftheForm.Contains(searchstring)).ToList());
            }
            else
            return View(_context.Forms.ToList());
        }
        public IActionResult UserFormPage(int? id)
        {
            List<QuestionSheet> listofQuestions = _context.QuestionSheet.Where(x => x.FormId == id).ToList();
            if (listofQuestions.Count() != 0)
            {
                return View(listofQuestions);
            }
            else
                //I create a form Id that i check inside the view and if it is 0 then I know that the list is empty
                return View(listofQuestions = new List<QuestionSheet>()
                {
                    new QuestionSheet()
                    {
                       FormId=0
                    }
                });
        }

        public IActionResult FormCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FormCreate(Form obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                _context.SaveChanges();
                return RedirectToAction(nameof(SettingsForForms));
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Forms == null)
            {
                return NotFound();
            }

            var form = _context.Forms
                .FirstOrDefault(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }
        [HttpPost]
        //DOESNT WORK YET -> BECAUSE OF PARENTID
        //ERROR -> IF IT HAS A FILLED SHEET
        public IActionResult Delete(int id)
        {
            //need to make a List from the questions
            //of the form that you want to delete and and check thoose questions Ids
            //if they exist in the answer table as foreign keys
            var form = _context.Forms.Find(id);

            List<QuestionSheet> itemsQustionExist = _context.QuestionSheet.Where(p => p.FormId == id).ToList();
            //List<int> QuestionIdList = new();
            //for (int i = 0; i < itemsQustionExist.Count(); i++)
            //{
            //    QuestionIdList.Add(itemsQustionExist[i].Id);
            //}

            if (!itemsQustionExist.Any())
            {
                if (form != null)
                {
                    _context.Forms.Remove(form);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(SettingsForForms));
            }
            else
            {
                //need to change the answer table as well
                //before removing the questions from the Questionsheet table
                List<Answer> listOfAnswersWithQuestionIds = new List<Answer>();
                
                for (int i = 0; i < itemsQustionExist.Count(); i++)
                {
                    listOfAnswersWithQuestionIds=_context.Answers.Where(a => a.QuestionId == itemsQustionExist[i].Id).ToList();
                    for (int j = 0; j < listOfAnswersWithQuestionIds.Count(); j++)
                    {
                        listOfAnswersWithQuestionIds[j].QuestionId = null;
                        _context.Update(listOfAnswersWithQuestionIds[j]);
                        _context.SaveChanges();
                    }
                }
                //removing the existing questions which is related to that form
                for (int i = 0; i < itemsQustionExist.Count(); i++)
                {
                    _context.QuestionSheet.Remove(itemsQustionExist[i]);

                }
                _context.SaveChanges();
            }
            if (form != null)
            {
                _context.Forms.Remove(form);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(SettingsForForms));


            //List<QuestionSheet> itemsQustionExist = _context.QuestionSheet.Where(p=>p.FormId == id).ToList();
            //List<int> QuestionIdList = new();
            //for (int i = 0; i < itemsQustionExist.Count(); i++)
            //{
            //    QuestionIdList.Add(itemsQustionExist[i].Id);
            //}
            //List<Answer> itemAnswerExist = _context.Answers.Where(p => QuestionIdList.Contains((int)p.QuestionId)).ToList();
            //for (int i = 0; i < itemAnswerExist.Count; i++)
            //{

            //}

            //if (!itemsQustionExist.Any())
            //{
            //    if (form != null)
            //    {
            //        _context.Forms.Remove(form);
            //    }
            //    _context.SaveChanges();
            //    return RedirectToAction(nameof(SettingsForForms));
            //}

            //else
            //{

            //    for (int i = 0; i < itemsQustionExist.Count(); i++)
            //    {
            //        _context.QuestionSheet.Remove(itemsQustionExist[i]);

            //    }
            //}
            //_context.SaveChanges();
            //return RedirectToAction(nameof(SettingsForForms));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Forms == null)
            {
                return NotFound();
            }

            var forms = _context.Forms.Find(id);
            if (forms == null)
            {
                return NotFound();
            }
            return View(forms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Form obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obj);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(obj.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SettingsForForms));
            }
            return View(obj);
        }
        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}
