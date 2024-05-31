using PA_project.Models;
using PA_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_project.Controllers
{
    public class ReviewController : Controller
    {

        private LibraryDBContext db;

        public ReviewController()
        {
            db = new LibraryDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [HttpPost]
        public ActionResult AddReview(int bookId, string commentText)
        {
            if (!string.IsNullOrWhiteSpace(commentText))
            {
                var review = new Review
                {
                    BookId = bookId,
                    Text = commentText,
                    CreationDate = DateTime.Now,
                    UserId = LoginHelper.Instance.UserId
                };

                db.Reviews.Add(review);
                db.SaveChanges();

            }

            return RedirectToAction("SingleBookWithComments", "Book", new { bookId });
        }
    }
}