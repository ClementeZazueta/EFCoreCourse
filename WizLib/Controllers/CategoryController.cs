using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> objList = _context.Categories.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            var obj = new Category();

            if (id == null)
            {
                return View(obj);
            }

            //For edit
            obj = _context.Categories.FirstOrDefault(c => c.Category_Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Category_Id == 0)
                {
                    //Create
                    _context.Categories.Add(obj);
                }
                else
                {
                    //Update
                    _context.Categories.Update(obj);
                }

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            var obj = _context.Categories.FirstOrDefault(c => c.Category_Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 2; i++)
            {
                categories.Add(new Category { Name = Guid.NewGuid().ToString() });
                //_context.Categories.Add(new Category
                //{
                //    Name = Guid.NewGuid().ToString()
                //});
            }

            _context.AddRange(categories);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }

            _context.AddRange(categories);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {
            var categories = _context.Categories
                .OrderByDescending(c => c.Category_Id)
                .Take(2)
                .ToList() as IEnumerable<Category>;

            _context.RemoveRange(categories);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            var categories = _context.Categories
                .OrderByDescending(c => c.Category_Id)
                .Take(5)
                .ToList() as IEnumerable<Category>;

            _context.RemoveRange(categories);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
