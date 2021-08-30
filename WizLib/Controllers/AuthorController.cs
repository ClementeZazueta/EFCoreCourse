using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var authors = _context.Fluent_Authors.ToList();
            return View(authors);
        }

        public IActionResult Upsert(int? id)
        {
            var author = new Fluent_Author();

            if (id == null)
            {
                return View(author);
            }

            author = _context.Fluent_Authors.FirstOrDefault(a => a.Author_Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Fluent_Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            if (author.Author_Id == 0)
            {
                author.BirthDate = Convert.ToDateTime(author.BirthDate.Value.ToString("yyyy/MM/dd"));
                _context.Fluent_Authors.Add(author);
            }
            else
            {
                _context.Fluent_Authors.Update(author);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var authors = _context.Fluent_Authors.FirstOrDefault(a => a.Author_Id == id);

            if (authors == null)
            {
                return NotFound();
            }
            _context.Fluent_Authors.Remove(authors);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
