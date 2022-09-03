using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Data;

namespace PersonalBlog.Controllers
{
    public class AdminLoginController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginCheck(LoginCheckRequest request)
        {
            BlogDBEntities db = new BlogDBEntities();
            User user = db.User.Where(p => p.Username == request.Username && p.Password == request.Password).SingleOrDefault();
            if (user==null)
            {

                Session[SessionKeyManager.ErrorMessageKey] = "Invalid Username and/or Password";

                return RedirectToAction("Index");
            }
            else
            {
                Session[SessionKeyManager.LoginKey] = user;
                return RedirectToAction("Index", "Admin");
            }
           
        }


        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}