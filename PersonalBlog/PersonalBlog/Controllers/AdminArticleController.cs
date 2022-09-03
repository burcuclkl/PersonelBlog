using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class AdminArticleController : BaseLoginController
    {
  
        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Articles = db.Article.OrderByDescending(p => p.PublishDate).ToList();
            return View();
        }

        public ActionResult Add()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Categories = db.Category.OrderBy(p => p.Name).ToList();
            return View();
        }


        [HttpPost]
        public ActionResult AddArticle(AddArticleRequest request)
        {
            User user = Session[SessionKeyManager.LoginKey] as User;
            Article article = new Article();
            article.CategoryId = request.CategoryId;
            article.Content = request.Content;
            article.PreContent = request.PreContent;
            article.RateCount = 0;
            article.RateValue = 0;
            article.Title = request.Title;
            article.UserId = user.Id;
            article.PublishDate = DateTime.Now;
            article.IsActive = true;
            BlogDBEntities db = new BlogDBEntities();
            db.Article.Add(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeArticleState()
        {
            int articleId = Convert.ToInt32(Request.QueryString["articleId"]);
            string state = Request.QueryString["state"];
            BlogDBEntities db = new BlogDBEntities();
            Article article = db.Article.Where(p => p.Id == articleId).SingleOrDefault();
            if (state=="0")
            {
                article.IsActive = false;
            }
            else if (state=="1")
            {
                article.IsActive = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update()
        {
            int id = Convert.ToInt32(Request.QueryString["articleId"]);
            BlogDBEntities db = new BlogDBEntities();
            Article article = db.Article.Where(p => p.Id == id).SingleOrDefault();
            ViewBag.Categories = db.Category.ToList();
            return View(article);
        }

        [HttpPost]
        public ActionResult UpdateArticle(UpdateArticleRequest request)
        {
            BlogDBEntities db = new BlogDBEntities();
            Article updateArticle = db.Article.Where(p => p.Id == request.Id).SingleOrDefault();
            updateArticle.Title = request.Title;
            updateArticle.PreContent = request.PreContent;
            updateArticle.Content = request.Content;
            updateArticle.CategoryId = request.CategoryId;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}