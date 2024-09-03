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
    public class QuestionSheetController : Controller
    {
        private readonly formBuilderContext _context;
        SheetVM sheetVM;

        //For parent-child questions -> Create Action
        CreateSheetVM createQuestion;

        //For parent-child questions -> Edit Action
        CreateSheetVM modifyQuestion;

        public QuestionSheetController(formBuilderContext context)
        {
            _context = context;
        }
        public IActionResult StatementPage()
        {
            return View(_context.Statements.ToList());
        }
        [HttpGet]
        public IActionResult StatementCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StatementCreate(Statement obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                _context.SaveChanges();
                return Redirect("~/QuestionSheet/StatementPage/");
            }
            return View(obj);
        }
        public IActionResult AdminQuestionSheetControl(int? id,string searcString)
        {
            List<QuestionSheet> listofQuestions = new List<QuestionSheet>();
            //if (!string.IsNullOrEmpty(searcString))
            //{
            //    listofQuestions = _context.QuestionSheet.Where(x => x.FormId == id && x.Question.Contains(searcString)).ToList();
            //    return View(listofQuestions);
            //}
            //else
            //{
            //    listofQuestions = _context.QuestionSheet.Where(x => x.FormId == id).ToList();
            //}

            //searchstring doesnt work -> it you can see it in the url
            //but the parameter inside controller is null it doesnt refresh it
            listofQuestions = _context.QuestionSheet.Where(x => x.FormId == id).ToList();
            if (listofQuestions.Count() != 0)
            {
                if (!string.IsNullOrEmpty(searcString))
                {
                    listofQuestions = _context.QuestionSheet.Where(x => x.FormId == id && x.Question.Contains(searcString)).ToList();
                    return View(listofQuestions);
                }
                else
                return View(listofQuestions);
            }
            else
            {
                return View(listofQuestions = new List<QuestionSheet>()
                {
                    new QuestionSheet()
                    {
                       //FormId=0
                       Id=0,
                       FormId=id,
                       formName=_context.Forms.Where(x=>x.Id==id).Select(x=>x.nameOftheForm).First()
                    }
                });
            }
        }
        public IActionResult Create(int? FormId)
        {
            //implementation for the child-parent questions
            createQuestion = new CreateSheetVM();
            createQuestion.question = new QuestionSheet();
            createQuestion.question.FormId = FormId;
            createQuestion.question.formName = _context.Forms.Find(FormId).nameOftheForm;

            createQuestion.everythingExceptParent = _context.QuestionSheet.Where(x => (x.typeOfQuestion == "parentChild" || x.typeOfQuestion == "child") && x.FormId == FormId && x.parentQuestionId == null).ToList().ConvertAll(a =>
            new SelectListItem()
            {
                Text = a.Question.ToString(),
                Value = a.Id.ToString()
            }); 

            //first the parent/parent-child question options

            // createQuestion.everythingExceptChild = _context.QuestionSheet.Where(x => (x.typeOfQuestion == "parent" || x.typeOfQuestion == "parentChild") && x.FormId == FormId).ToList().ConvertAll(a =>
            //new SelectListItem()
            //{
            //    Text = a.Question.ToString(),
            //    Value = a.Id.ToString()
            //});

            createQuestion.StatementList = _context.Statements.ToList().ConvertAll(a =>
           new SelectListItem()
           {
               Text = a.Name.ToString(),
               Value = a.Id.ToString()
           });

            //if they pick child you just save it

            return View(createQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSheetVM obj)
        {
            var current = obj;
            //Has to add the Newly created question first because before adding it its Id is 0
            if (ModelState.IsValid)
            {
                _context.QuestionSheet.Add(current.question);
                _context.SaveChanges();
            }
            //If the newly added question is parent then
            //We have to set the selected Question`s ParentId to the newly created one
            if (current.question.typeOfQuestion == "parent")
            {
                QuestionSheet temp = new QuestionSheet();
                temp = _context.QuestionSheet.Find(current.question.childQuestionId);
                temp.parentQuestionId = current.question.Id;
                _context.Update(temp);
                _context.SaveChanges();
            }
            if (current.question.typeOfQuestion == "parentChild")
            {
                //QuestionSheet tempParent = new QuestionSheet();
                QuestionSheet tempChild = new QuestionSheet();
                //tempParent = _context.QuestionSheet.Find(current.question.parentQuestionId);
                tempChild = _context.QuestionSheet.Find(current.question.childQuestionId);
                //tempParent.childQuestionId= current.question.Id;
                tempChild.parentQuestionId = current.question.Id;
                //_context.Update(tempParent);
                _context.Update(tempChild);
                _context.SaveChanges();
            }
            return Redirect("~/QuestionSheet/AdminQuestionSheetControl/" + current.question.FormId);

        }
        public IActionResult Edit(int? id, int? FormId)
        {
            modifyQuestion = new CreateSheetVM();
            modifyQuestion.question = new QuestionSheet();
            modifyQuestion.question.FormId = FormId;
            modifyQuestion.question.formName = _context.Forms.Find(FormId).nameOftheForm;
            modifyQuestion.everythingExceptParent = _context.QuestionSheet.Where(x => (x.typeOfQuestion == "parentChild" || x.typeOfQuestion == "child") && x.FormId == FormId && x.parentQuestionId == null).ToList().ConvertAll(a =>
            new SelectListItem()
            {
                Text = a.Question.ToString(),
                Value = a.Id.ToString()
            });
            //if they pick parent-child
            //first the parent/parent-child question options

            // modifyQuestion.everythingExceptChild = _context.QuestionSheet.Where(x => (x.typeOfQuestion == "parent" || x.typeOfQuestion == "parentChild") && x.FormId == FormId).ToList().ConvertAll(a =>
            //new SelectListItem()
            //{
            //    Text = a.Question.ToString(),
            //    Value = a.Id.ToString()
            //});
            modifyQuestion.StatementList = _context.Statements.ToList().ConvertAll(a =>
          new SelectListItem()
          {
              Text = a.Name.ToString(),
              Value = a.Id.ToString()
          });
            //if they pick child you just save it
            modifyQuestion.question = _context.QuestionSheet.Find(id);
            return View(modifyQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateSheetVM obj)
        {
            obj.question.formName = _context.Forms.Find(obj.question.FormId).nameOftheForm;

            if (obj.question.typeOfQuestion == "only")
            {
                QuestionSheet tempParentQuestion = new QuestionSheet();
                QuestionSheet tempChildQuestion = new QuestionSheet();
                tempChildQuestion = _context.QuestionSheet.Where(x => x.parentQuestionId == obj.question.Id).FirstOrDefault();
                tempParentQuestion = _context.QuestionSheet.Where(x => x.childQuestionId == obj.question.Id).FirstOrDefault();

                if (tempParentQuestion != null)
                {
                    tempParentQuestion.childQuestionId = null;
                    _context.Update(tempParentQuestion);
                }
                if (tempChildQuestion != null)
                {
                    tempChildQuestion.parentQuestionId = null;
                    _context.Update(tempChildQuestion);
                }
                _context.SaveChanges();
            }
            else if (obj.question.typeOfQuestion == "child")
            {
                QuestionSheet tempParentQuestion = new QuestionSheet();
                QuestionSheet tempChildQuestion = new QuestionSheet();
                tempChildQuestion = _context.QuestionSheet.Where(x => x.parentQuestionId == obj.question.Id).FirstOrDefault();
                tempParentQuestion = _context.QuestionSheet.Where(x => x.childQuestionId == obj.question.Id).FirstOrDefault();

                if (tempParentQuestion != null)
                {
                    tempParentQuestion.childQuestionId = null;
                    _context.Update(tempParentQuestion);
                }
                if (tempChildQuestion != null)
                {
                    tempChildQuestion.parentQuestionId = null;
                    _context.Update(tempChildQuestion);
                }
                _context.SaveChanges();
            }

            else if (obj.question.typeOfQuestion == "parentChild")
            {
                QuestionSheet tempChild = new QuestionSheet();
                tempChild = _context.QuestionSheet.Find(obj.question.childQuestionId);
                tempChild.parentQuestionId = obj.question.Id;
                _context.Update(tempChild);
                _context.SaveChanges();
            }
            else if (obj.question.typeOfQuestion == "parent")
            {
                QuestionSheet temp = new QuestionSheet();
                temp = _context.QuestionSheet.Find(obj.question.childQuestionId);
                temp.parentQuestionId = obj.question.Id;
                _context.Update(temp);
                _context.SaveChanges();
            }
            _context.Update(obj.question);
            _context.SaveChanges();
            return Redirect("~/QuestionSheet/AdminQuestionSheetControl/" + obj.question.FormId);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuestionSheet == null)
            {
                return NotFound();
            }

            var questionSheet = await _context.QuestionSheet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionSheet == null)
            {
                return NotFound();
            }

            return View(questionSheet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int countAnswerDb = _context.Answers.ToList().Count();
            if (_context.QuestionSheet == null)
            {
                return Problem("Entity set 'formBuilderContext.QuestionSheet'  is null.");
            }
            var questionSheet =  _context.QuestionSheet.Find(id);
            string routeForm = questionSheet.FormId.ToString();
            if (questionSheet != null && countAnswerDb == 0)
            {
                _context.QuestionSheet.Remove(questionSheet);
            }
            else
            {
                List<Answer> AnswersList = _context.Answers.Where(x => x.QuestionId == id).ToList();
                for (int i = 0; i < AnswersList.Count(); i++)
                {
                    AnswersList[i].QuestionId = null;
                }
                _context.QuestionSheet.Remove(questionSheet);
            }
            _context.SaveChanges();
            return Redirect("~/QuestionSheet/AdminQuestionSheetControl/" + routeForm);
        }
        public IActionResult Fill(int? id)
        {
            sheetVM = new SheetVM();
            //sheetVM.QuestionSheetList = _context.QuestionSheet.ToList();
            sheetVM.QuestionSheetList = _context.QuestionSheet.Where(x => x.FormId == id).ToList();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            sheetVM.User = claim.Value;
            return View(sheetVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Fill(SheetVM obj)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in obj.AnswersList)
                {
                    item.nameOfTheQuestionSheet = obj.sheetName;
                    _context.Answers.Add(item);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Answers));
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult Answers()
        {
            //AnswerVM answerVM = new AnswerVM();
            //answerVM.AnswersList = _context.Answers.ToArray();
            //answerVM.User = new();
            //answerVM.Questions = new();
            //for (int i = 0; i < answerVM.AnswersList.Length; i++)
            //{
            //    //This is where we fill the User list with the UserÏd
            //    answerVM.User.Add(_context.Users.Find(answerVM.AnswersList[i].UserTableId).Name);
            //    //this is where we fill the Question List with the question IDs
            //    answerVM.Questions.Add(_context.QuestionSheet.Find(answerVM.AnswersList[i].QuestionId).Question);
            //}
            //return View(answerVM);
            List<Answer> AnswersList = _context.Answers.ToList();
            return View(AnswersList);
        }

        public IActionResult Check(string? name)
        {
            AnswerVM answerVM = new AnswerVM();
            answerVM.AnswersList = _context.Answers.Where(x => x.nameOfTheQuestionSheet == name).ToArray();
            answerVM.User = new();
            answerVM.Questions = new();
            for (int i = 0; i < answerVM.AnswersList.Length; i++)
            {
                //This is where we fill the User list with the UserId
                if (answerVM.AnswersList[i].UserTableId != null)
                {
                    answerVM.User.Add(_context.Users.Find(answerVM.AnswersList[i].UserTableId).Name);
                }
                else
                {
                    answerVM.User.Add("The user has been deleted");
                }
                //this is where we fill the Question List with the question IDs
                if (answerVM.AnswersList[i].QuestionId != null)
                {
                    answerVM.Questions.Add(_context.QuestionSheet.Find(answerVM.AnswersList[i].QuestionId).Question);
                }
                else
                {
                    answerVM.Questions.Add("The question has been deleted");
                }
            }
            return View(answerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAnswers(int id)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in _context.Answers.ToList())
                {
                    _context.Answers.Remove(item);
                }
                _context.SaveChanges();
            }
            return Redirect("~/Form/SettingsForForms");
        }
        private bool QuestionSheetExists(int id)
        {
            return _context.QuestionSheet.Any(e => e.Id == id);
        }
        #region API CALLS
        public IActionResult GetAll()
        {
            return Json(new { data = _context.Forms.ToList() });
        }

        #endregion
    }
}
