using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using WizLib_Model.Models;

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
                .Include(b => b.Fluent_BookAuthor)
                .ThenInclude(ba => ba.Fluent_Author)
                .ToList();

            #region Explicit loading
            //var books = _context.Fluent_Books.ToList();
            //foreach (var b in books)
            //{
            //    //Least efficient
            //    //b.Fluent_Publisher = _context.Fluent_Publishers.FirstOrDefault(p => p.Publisher_Id == b.Publisher_Id);

            //    //Explicit loading more efficient
            //    _context.Entry(b).Reference(p => p.Fluent_Publisher).Load();
            //    _context.Entry(b).Collection(a => a.Fluent_BookAuthor).Load();

            //    foreach (var bookAuth in b.Fluent_BookAuthor)
            //    {
            //        _context.Entry(bookAuth).Reference(ba => ba.Fluent_Author).Load();
            //    }
            //}
            #endregion
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

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorViewModel obj = new()
            {
                BookAuthorList = _context.Fluent_BookAuthors
                    .Include(ba => ba.Fluent_Author)
                    .Include(ba => ba.Fluent_Book)
                    .Where(ba => ba.Book_Id == id)
                    .ToList(),

                BookAuthor = new Fluent_BookAuthor
                {
                    Book_Id = id
                },

                Book = _context.Fluent_Books.FirstOrDefault(b => b.Book_Id == id)
            };

            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(ba => ba.Author_Id).ToList();

            //NOT IN clause in LINQ
            var tempList = _context.Fluent_Authors.Where(a => !tempListOfAssignedAuthors.Contains(a.Author_Id)).ToList();

            obj.AuthorList = tempList.Select(tl => new SelectListItem 
            {
                Text = tl.FullName,
                Value = tl.Author_Id.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorViewModel bookAuthorViewModel)
        {
            if (bookAuthorViewModel.BookAuthor.Book_Id != 0 && bookAuthorViewModel.BookAuthor.Author_Id != 0)
            {
                _context.Fluent_BookAuthors.Add(bookAuthorViewModel.BookAuthor);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorViewModel.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorViewModel bookAuthorViewModel)
        {
            int bookId = bookAuthorViewModel.Book.Book_Id;
            Fluent_BookAuthor bookAuthor = _context.Fluent_BookAuthors
                .FirstOrDefault(ba => ba.Author_Id == authorId && ba.Book_Id == bookId);

            _context.Fluent_BookAuthors.Remove(bookAuthor);
            _context.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Fluent_Books.FirstOrDefault(a => a.Book_Id == id);

            _context.Fluent_Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult PlayGround()
        {
            //var bookTemp = _context.Fluent_Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _context.Fluent_Books;
            //double totalPrice = 0.0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _context.Fluent_Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _context.Fluent_Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _context.Fluent_Books.Count();

            IEnumerable<Fluent_Book> bookList1 = _context.Fluent_Books;
            List<Fluent_Book> filteredBook1 = bookList1.Where(b => b.Price > 500).ToList();

            IQueryable<Fluent_Book> bookList2 = _context.Fluent_Books;
            List<Fluent_Book> filteredBook2 = bookList2.Where(b => b.Price > 500).ToList();

            Fluent_Book bookTemp1 = _context.Fluent_Books
                .Include(b => b.Fluent_BookDetail)
                .FirstOrDefault(b => b.Book_Id == 3);
            bookTemp1.Fluent_BookDetail.NumberOfChapters = 11;

            _context.Fluent_Books.Update(bookTemp1);
            _context.SaveChanges();

            Fluent_Book bookTemp2 = _context.Fluent_Books
                .Include(b => b.Fluent_BookDetail)
                .FirstOrDefault(b => b.Book_Id == 3);
            bookTemp2.Fluent_BookDetail.Weight = 1;

            _context.Fluent_Books.Attach(bookTemp2);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
