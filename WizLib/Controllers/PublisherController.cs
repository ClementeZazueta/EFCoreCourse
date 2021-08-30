using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublisherController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var publishers = _context.Fluent_Publishers.ToList();
            return View(publishers);
        }

        public IActionResult Upsert(int? id)
        {
            var publisher = new Fluent_Publisher();

            if (id == null)
            {
                return View(publisher);
            }

            publisher = _context.Fluent_Publishers.FirstOrDefault(a => a.Publisher_Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Fluent_Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            if (publisher.Publisher_Id == 0)
            {
                _context.Fluent_Publishers.Add(publisher);
            }
            else
            {
                _context.Fluent_Publishers.Update(publisher);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var publisher = _context.Fluent_Publishers.FirstOrDefault(a => a.Publisher_Id == id);

            if (publisher == null)
            {
                return NotFound();
            }
            _context.Fluent_Publishers.Remove(publisher);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}

