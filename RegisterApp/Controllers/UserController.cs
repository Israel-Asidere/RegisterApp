using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace RegisterApp.Controllers
{

    public class UserController : Controller
    {
        public ActionResult AddOrEdit()
        {
            User usermodel = new User();
            using (RegisterContext db = new RegisterContext())
            {
                usermodel.PositionCollection = db.Pos.ToList<Pos>();
            }
            return View(  usermodel);
        }
        [HttpPost]
        public ActionResult AddOrEdit(User user)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }
        RegisterContext db = new RegisterContext();
        public ActionResult Index(User userModel)
        {
            
                return View(db.Users.ToList());   
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }
        [HttpGet]
        public ActionResult Edit([Bind(Include = "UserName, UserPosID, IsUserPresent,Date,TimeI,TimeOut")]int? id)
        {
            RegisterContext db = new RegisterContext();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Single(reg => reg.UserID == id);
            if (user == null)
                return HttpNotFound();
            using (db)
            {
                user.PositionCollection = db.Pos.ToList<Pos>();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection formCollection, int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OrderByDate()
        {
            var user = from p in db.Users
                       orderby p.Date
                       select p;
            return View(user);

        }
        public ActionResult OrderByName()
        {
            var user = from p in db.Users
                       orderby p.UserName
                       select p;
            return View(user);

        }
        public ActionResult OrderByPresent()
        {
            var user = from p in db.Users
                       orderby p.IsUserPresent
                       select p;
            return View(user);

        }
    }
}