using PA_project.Models;
using PA_project.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PA_project.Controllers
{
    public class BookController : Controller
    {
        private LibraryDBContext db;

        public BookController()
        {
            db = new LibraryDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult MyBooks(int userId)
        {
            if(userId == -1)
            {
                return RedirectToAction("Login", "User");
            }
            var books = db.Books.Where(b => b.UserId == userId).ToList();
            return View(books);
        }

        public ActionResult SingleBook(int bookId)
        {
            var book = db.Books.FirstOrDefault(b => b.BookId == bookId);
            return View(book);
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook([Bind(Include = "Title, Author, Description, Genre, PublicationYear, ImageUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                int userId = LoginHelper.Instance.UserId;
                book.UserId = userId;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("MyBooks", "Book", new { userId = userId });
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook([Bind(Include = "BookId, Title, Author, Description, Genre, PublicationYear, ImageUrl, UserId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyBooks", "Book", new { userId = book.UserId });
            }

            return View(book);
        }

        public ActionResult EditBook(int bookId)
        {
            Book book = db.Books.Find(bookId);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int bookId)
        {
            Book book = db.Books.Find(bookId);
            int userId = LoginHelper.Instance.UserId;
            List<CollectionBooks> cb = db.CollectionBooks.Where(c => c.BooksId == bookId).ToList();
            List<Review> reviews = db.Reviews.Where(r => r.BookId == bookId).ToList();

            db.Books.Remove(book);
            db.CollectionBooks.RemoveRange(cb);
            db.Reviews.RemoveRange(reviews);
            db.SaveChanges();
            return RedirectToAction("MyBooks", "Book", new { userId = LoginHelper.Instance.UserId });
        }

        public ActionResult AddToCollection()
        {
            string bookId = Request.QueryString["bookId"];
            string selectedCollectionId = Request.QueryString["selectedCollectionId"];

            int convertedBookId = Convert.ToInt32(bookId);
            int convertedCollectionId = Convert.ToInt32(selectedCollectionId);

            CollectionBooks cb = db.CollectionBooks.Where(c => c.BooksId == convertedBookId && c.CollectionId == convertedCollectionId).FirstOrDefault();

            if(cb != null)
            {
                return RedirectToAction("SingleCollection", "Collection", new { collectionId = Convert.ToInt32(selectedCollectionId) });
            }
            cb = new CollectionBooks
            {
                CollectionId = convertedCollectionId,
                BooksId = convertedBookId
            };

            db.CollectionBooks.Add(cb);
            db.SaveChanges();
            return RedirectToAction("SingleCollection", "Collection", new { collectionId = Convert.ToInt32(selectedCollectionId) });
        }

        [HttpPost]
        public ActionResult AddToCollection(int bookId, int selectedCollectionId)
        {
            CollectionBooks cb = new CollectionBooks
            {
                CollectionId = selectedCollectionId,
                BooksId = bookId
            };

            db.CollectionBooks.Add(cb);
            db.SaveChanges();
            return RedirectToAction("SingleCollection", "Collection", new { selectedCollectionId });
        }

        public ActionResult UniversalBooks()
        {
            int userId = LoginHelper.Instance.UserId;
            if (userId == -1)
            {
                return RedirectToAction("Login", "User");
            }
            List<Book> allBooks = db.Books.Where(b => b.UserId != userId).ToList();
            return View(allBooks);
        }

        public ActionResult SingleBookWithComments(int bookId)
        {
            List<Review> reviews = db.Reviews.Where(r => r.BookId == bookId).ToList();
            return View(reviews);
        }
    }
}