using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Fluent_Books
                .Include(b => b.Fluent_Publisher)
                .ToList();
            //foreach (var b in books)
            //{
            //    //Least efficient
            //    //b.Fluent_Publisher = _context.Fluent_Publishers.FirstOrDefault(p => p.Publisher_Id == b.Publisher_Id);

            //    //Explicit loading more efficient
            //    _context.Entry(b).Reference(p => p.Fluent_Publisher).Load();
            //}
            return View(books);
        }

        public IActionResult Details(int? id)
        {
            var booksViewModel = new BookViewModel();

            if (id == null)
            {
                return View(booksViewModel);
            }

            booksViewModel.Book = _context.Fluent_Books
                .Include(b => b.Fluent_BookDetail)
                .FirstOrDefault(a => a.Book_Id == id);
            //booksViewModel.Book.Fluent_BookDetail = _context.Fluent_BookDetails

            if (booksViewModel == null)
            {
                return NotFound();
            }

            return View(booksViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookViewModel bookViewModel)
        {
            if (bookViewModel.Book.Fluent_BookDetail.BookDetail_Id== 0)
            {
                _context.Fluent_BookDetails.Add(bookViewModel.Book.Fluent_BookDetail);
                _context.SaveChanges();

                var lastBook = _context.Fluent_Books.FirstOrDefault(b => b.Book_Id == bookViewModel.Book.Book_Id);
                lastBook.BookDetail_Id = bookViewModel.Book.Fluent_BookDetail.BookDetail_Id;
            }
            else
            {
                _context.Fluent_BookDetails.Update(bookViewModel.Book.Fluent_BookDetail);
                _context.SaveChanges();
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Upsert(int? id)
        {
            var booksViewModel = new BookViewModel();
            booksViewModel.PublisherList = _context.Fluent_Publishers.Select(p => new SelectListItem
            {
                Value = p.Publisher_Id.ToString(),
                Text = p.Name
            }).ToList();

            if (id == null)
            {
                return View(booksViewModel);
            }

            booksViewModel.Book = _context.Fluent_Books.FirstOrDefault(a => a.Book_Id == id);

            if (booksViewModel == null)
            {
                return NotFound();
            }

            return View(booksViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookViewModel bookViewModel)
        {
            if (bookViewModel.Book.Book_Id == 0)
            {
                _context.Fluent_Books.Add(bookViewModel.Book);
            }
            else
            {
                _context.Fluent_Books.Update(bookViewModel.Book);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Fluent_Books.FirstOrDefault(a => a.Book_Id == id);

            _context.Fluent_Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
