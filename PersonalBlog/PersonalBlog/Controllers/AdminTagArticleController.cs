using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class AdminTagArticleController : BaseLoginController
    {

        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Tags = db.Tag.ToList();
            ViewBag.Articles = db.Article.ToList();
            ViewBag.TagArticle = db.TagArticle.OrderBy(p => p.ArticleId).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddTagArticle(AddTagArticleRequest request)
        {
            //tag kontrol
            BlogDBEntities db = new BlogDBEntities();
            TagArticle tagArticle = db.TagArticle.Where(p => p.TagId == request.TagId && p.ArticleId == request.ArticleId).FirstOrDefault();
            if (tagArticle == null)
            {
                TagArticle insertTagArticle = new TagArticle();
                insertTagArticle.TagId = request.TagId;
                insertTagArticle.ArticleId = request.ArticleId;
                db.TagArticle.Add(insertTagArticle);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            BlogDBEntities db = new BlogDBEntities();
            TagArticle tagArticle = db.TagArticle.Where(p => p.Id == id).SingleOrDefault();
            db.TagArticle.Remove(tagArticle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}