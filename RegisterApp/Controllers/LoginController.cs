using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegisterApp.Controllers
{
    public class LoginController : Controller
    { RegisterContext db = new RegisterContext();
        public ActionResult Index()
        {
            return View();
        }

        //for 'add user' 'new user'
        public ActionResult NewUser(Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Owners.Add(owner);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(owner);

        }

        public ActionResult Check()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Authorize(RegisterApp.Owner userModel)
        {
            using (RegisterContext db = new RegisterContext())
            {
                Owner userDetails = db.Owners.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password";
                }
                else
                {
                    Session["OwnerID"] = userDetails.OwnerID;
                    Session["OwnerName"] = userDetails.UserName;
                    Owner usrDetails = db.Owners.Where(x => x.OwnerID == userModel.OwnerID).FirstOrDefault();
                    if (usrDetails == null)
                    {
                        //return new instance of the Index view
                        ViewBag.userName = userDetails.UserName;
                        return RedirectToAction("Index", "User");

                    }
                    else
                    {
                        return View(db.Users.ToList());
                    }

                }
            }
            return View("Index", userModel);
        }
    }
}