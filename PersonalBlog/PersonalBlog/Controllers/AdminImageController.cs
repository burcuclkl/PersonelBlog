using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models;
using System.IO;

namespace PersonalBlog.Controllers
{
    public class AdminImageController : BaseLoginController
    {

        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Articles = db.Article.ToList();
            ViewBag.Images = db.ArticleImages.ToList();
            db.SaveChanges();
            return View();
        }


        public ActionResult AddImage(AddImageRequest request)
        {
            string folderName = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
            string mainFolderName = "Images";
            DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath("~/" + mainFolderName));
            DirectoryInfo saveDirectory=  directoryInfo.GetDirectories().Where(p => p.Name == folderName).SingleOrDefault();
            if (saveDirectory == null)
            {
                directoryInfo.CreateSubdirectory(folderName);
                
            }

            BlogDBEntities db = new BlogDBEntities();
            foreach (HttpPostedFileBase file in request.Images)
            {
               string[] contents = file.ContentType.Split('/');

                if (contents[0].Contains("image"))
                {
                    //resim dosyası oldg. emin olduk
                    //1-dosya kontrolü,2-resmin dosyaya kayıt edilmesi,3-bilgilerin database kayıt edilmesi
                    string fileName = Guid.NewGuid() + "_" + file.FileName;
                    file.SaveAs(Server.MapPath("~/" + mainFolderName + "/" + folderName + "/" + fileName));

                    ArticleImages articleImages = new ArticleImages();
                    articleImages.ArticleId = request.ArticleId;
                    articleImages.Path = mainFolderName + "/" + folderName + "/" + fileName;
                    articleImages.Size = file.ContentLength;
                    articleImages.Extension = contents[1];
                    db.ArticleImages.Add(articleImages);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("Index");

        }
        public ActionResult Delete()
        {

            int imageId = Convert.ToInt32(Request.QueryString["id"]);
            BlogDBEntities db = new BlogDBEntities();
            ArticleImages image = db.ArticleImages.Where(p => p.Id == imageId).SingleOrDefault();

            FileInfo fileInfo = new FileInfo(Server.MapPath("~/" + image.Path));
            fileInfo.Delete();

            db.ArticleImages.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}