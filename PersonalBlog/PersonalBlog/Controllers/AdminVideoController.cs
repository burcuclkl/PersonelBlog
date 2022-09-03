using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class AdminVideoController : BaseLoginController
    {
        
        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Articles = db.Article.ToList();
            ViewBag.Videos = db.ArticleVideo.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddVideo(AddVideoRequest request)
        {
            ArticleVideo articleVideo = new ArticleVideo();
            articleVideo.ArticleId = request.ArticleId;
            articleVideo.Description = request.Description;
            articleVideo.Frame = request.Frame;

            BlogDBEntities db = new BlogDBEntities();
            db.ArticleVideo.Add(articleVideo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            int videoId = Convert.ToInt32(Request.QueryString["id"]);
            BlogDBEntities db = new BlogDBEntities();
            
            ArticleVideo articleVideo=db.ArticleVideo.Where(p => p.Id == videoId).SingleOrDefault();
            db.ArticleVideo.Remove(articleVideo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}