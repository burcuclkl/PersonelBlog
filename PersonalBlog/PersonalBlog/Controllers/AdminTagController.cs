using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Data;

namespace PersonalBlog.Controllers
{
    public class AdminTagController : BaseLoginController
    {
   
        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Tags = db.Tag.OrderBy(p => p.Content).ToList();
            return View();
        }

        public ActionResult AddTag(AddTagRequest request)
        {
            Tag newTag = new Tag();
            newTag.Content = ClearTagGenerator(request.Content);
            BlogDBEntities db = new BlogDBEntities();
            db.Tag.Add(newTag);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        private string ClearTagGenerator(string tagContent)
        {
            //küçük harf yap , başındaki ve sonundaki boşlukları al
            tagContent = tagContent.ToLower().Trim();


            //türkçe harflleri dönüştür
            tagContent = tagContent.Replace("ş", "s");
            tagContent = tagContent.Replace("ç", "c");
            tagContent = tagContent.Replace("ö", "o");
            tagContent = tagContent.Replace("ü", "u");
            tagContent = tagContent.Replace("ı", "i");
            tagContent = tagContent.Replace("ğ", "g");

            //boşluk
            tagContent = tagContent.Replace(" ", "_");

            //hashag=
            tagContent = "#" + tagContent;
            return tagContent;
        }

        public ActionResult DeleteTag()
        {
            int tagId = Convert.ToInt32(Request.QueryString["TagId"]);
            BlogDBEntities db = new BlogDBEntities();

            Tag tag = db.Tag.Where(p => p.Id == tagId).SingleOrDefault();
            db.Tag.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}