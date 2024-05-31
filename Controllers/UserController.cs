using PA_project.Models;
using PA_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_project.Controllers
{
    public class UserController : Controller
    {

        private LibraryDBContext db;

        public UserController()
        {
            db = new LibraryDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(u => u.UserName.Equals(username) &&
                u.Password.Equals(password)).FirstOrDefault();

                if(user != null)
                {
                    LoginHelper.Instance.UserId = user.UserId;
                    LoginHelper.Instance.Username = user.UserName;
                    LoginHelper.Instance.ExistingCollections = db.Collections.Where(c => c.UserId == user.UserId).ToList();
                    return RedirectToAction("MyBooks", "Book", new {userId = user.UserId});
                }
            }
            return View();
        }

        private Collection CreateDefaultCollection(int userId)
        {
            User user = db.Users.Find(userId);
            Collection favCollection = new Collection
            {
                UserId = user.UserId,
                Title = user.FirstName + "'s favorite books",
                Description = "Default collection created for your favorite books",
                CreationDate = DateTime.Now,
                IconUrl = "favorite.png"
            };
            return favCollection;
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.Collections.Add(CreateDefaultCollection(user.UserId));

                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            LoginHelper.Instance.UserId = -1;
            return RedirectToAction("Login");
        }
    }
}
