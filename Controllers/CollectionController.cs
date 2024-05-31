using PA_project.Models;
using PA_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_project.Controllers
{
    public class CollectionController : Controller
    {
        private LibraryDBContext db;
        private int userId;

        public CollectionController()
        {
            db = new LibraryDBContext();
            userId = LoginHelper.Instance.UserId;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void resetExistingCollections()
        {
            LoginHelper.Instance.ExistingCollections = db.Collections.Where(c => c.UserId == userId).ToList();
        }

        public ActionResult MyCollections(int userId)
        {
            if (userId == -1)
            {
                return RedirectToAction("Login", "User");
            }

            List<Collection> collections = db.Collections.Where(c => c.UserId == userId).ToList();
            return View(collections);
        }

        public ActionResult AddCollection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCollection([Bind(Include = "Title, Description, IconUrl")] Collection collection) {

            if (ModelState.IsValid)
            {
                collection.UserId = userId;
                collection.CreationDate = DateTime.Now;
                db.Collections.Add(collection);
                db.SaveChanges();

                resetExistingCollections();
                return RedirectToAction("MyCollections", "Collection", new { userId });
            }
            return View();
        }

        public ActionResult SingleCollection(int collectionId)
        {
            Collection collection = db.Collections.Find(collectionId);
            LoginHelper.Instance.CurrentCollectionId =  collectionId; 

            List<CollectionBooks> collectionBooks = db.CollectionBooks.Where(c => c.CollectionId == collectionId).ToList();
            List<Book> booksInCollection = new List<Book>();
            foreach(CollectionBooks collectionBook in collectionBooks)
            {
                Book book = db.Books.Find(collectionBook.BooksId);
                booksInCollection.Add(book);
            }
            return View(booksInCollection);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteBookFromCollection(int bookId)
        {
            int currentCollectionId = LoginHelper.Instance.CurrentCollectionId;
            CollectionBooks cb = db.CollectionBooks.Where(c => c.BooksId == bookId && c.CollectionId == currentCollectionId).FirstOrDefault();

            db.CollectionBooks.Remove(cb);
            db.SaveChanges();
            return RedirectToAction("SingleCollection", "Collection", new { collectionId = currentCollectionId });
        }


        [HttpPost, ActionName("DeleteCollection")]
        public ActionResult DeleteCollection(int collectionId)
        {
            Collection collection = db.Collections.Find(collectionId);
            List<CollectionBooks> cb = db.CollectionBooks.Where(c => c.CollectionId == collectionId).ToList();

            db.Collections.Remove(collection);
            db.CollectionBooks.RemoveRange(cb);
            db.SaveChanges();

            resetExistingCollections();
            return RedirectToAction("MyCollections", "Collection", new { userId = LoginHelper.Instance.UserId });
        }
    }
}