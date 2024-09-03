using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using formBuilder.Data;
using formBuilder.Models;

namespace formBuilder.Controllers
{
    public class UserController : Controller
    {
        private readonly formBuilderContext _context;

        public UserController(formBuilderContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            int countAnswerDb = _context.Answers.ToList().Count();
            if (_context.Users == null)
            {
                return Problem("Entity set 'formBuilderContext.QuestionSheet'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null && countAnswerDb == 0)
            {
                _context.Users.Remove(user);
            }
            else
            {
                List<Answer> AnswerList = _context.Answers.Where(x => x.UserTableId == id).ToList();
                for (int i = 0; i < AnswerList.Count(); i++)
                {
                    AnswerList[i].UserTableId = null;
                }
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionSheetExists(int id)
        {
            return _context.QuestionSheet.Any(e => e.Id == id);
        }
    }
}
