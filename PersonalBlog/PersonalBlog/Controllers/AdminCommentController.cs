using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Data;
namespace PersonalBlog.Controllers
{
    public class AdminCommentController : BaseLoginController
    {
     
        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Comments = db.Comment.Where(p => p.IsConfirmed == false).ToList();
            return View();
        }

        public ActionResult ChangeCommentState()
        {
            int commentId = Convert.ToInt32(Request.QueryString["id"]);
            int type = Convert.ToInt32(Request.QueryString["type"]);

            BlogDBEntities db = new BlogDBEntities();
            Comment comment = db.Comment.Where(p => p.Id == commentId).SingleOrDefault();

            if (type==1)
            {
                comment.IsConfirmed = true;
                db.SaveChanges();

            }
            else if (type==0)
            {
                db.Comment.Remove(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}