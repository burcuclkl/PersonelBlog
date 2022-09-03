using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
 
    public class ClientController : Controller
    {
       

        public ActionResult Index()
        {
            
            BlogDBEntities db = new BlogDBEntities();
            ViewBag.Articles = db.Article.ToList();
            return View();
        }
        public ActionResult ArticleDetail()
        {

            CreateCaptcha();

            int articleId = Convert.ToInt32(Request.QueryString["articleId"]);
            BlogDBEntities db = new BlogDBEntities();
            Article article = db.Article.Where(p => p.Id == articleId).SingleOrDefault();
            ViewBag.Comments = db.Comment.Where(p => p.IsConfirmed == true && p.Article.Id == article.Id).ToList();
            return View(article);
        }

        public ActionResult ChangeRate()
        {
            int articleId = Convert.ToInt32(Request.QueryString["id"]);
            decimal rate = Convert.ToDecimal(Request.QueryString["rate"]);
            BlogDBEntities db = new BlogDBEntities();
            Article article = db.Article.Where(p => p.Id == articleId).SingleOrDefault();
            decimal dbRateCount = Convert.ToDecimal(article.RateCount);
            decimal dbRateValue = Convert.ToDecimal(article.RateValue);

            decimal newRateCount = dbRateCount + 1;
            article.RateCount = Convert.ToInt32(newRateCount);
            decimal value = (dbRateCount * dbRateValue + rate) / newRateCount;
            article.RateValue = value;
            db.SaveChanges();


            return RedirectToAction("ArticleDetail" , new { articleId = articleId });
        }

        private void CreateCaptcha()
        {
            string[] process = { "+", "-", "x", ":" };
            Random rnd = new Random();
            int number = rnd.Next(0, 4);
            string operation = process[number];
            string captcha = string.Empty;
            switch (operation)
            {
                case "+":
                    int number1 = rnd.Next(1, 21);
                    int number2 = rnd.Next(1, 21);
                    captcha=number1 +"+" + number2 + " = ?";
                    ViewData["Result"] = number1 + number2;
                    break;


                case "-":
                    int number3 = rnd.Next(10, 21);
                    int number4 = rnd.Next(1, 10);
                    captcha = number3 + "-" + number4 + " = ?";
                    ViewData["Result"] = number3 - number4;
                    break;


                case "x":
                    int number5 = rnd.Next(1, 6);
                    int number6 = rnd.Next(1, 6);
                    captcha = number5 + "x" + number6 + " = ?";
                    ViewData["Result"] = number5 * number6;
                    break;


                case ":":
                    int number7 = rnd.Next(10, 21);
                    int number8 = 0;
                    while (true)
                    {
                        number8 = rnd.Next(1, 21);
                        if (number7 % number8==0)
                        {
                            break;
                        }
                    }

                    captcha = number7 + ":" + number8 + " = ?";
                    ViewData["Result"] = number7 / number8;
                    break;

            }
            ViewData["Captcha"] = captcha;

        }

        public ActionResult SaveComment(SaveCommentRequest request)
        {

            Comment comment = new Comment();
            comment.CommentId= request.Id;
            comment.Content = request.Comment;
            comment.Email = request.Email;
            comment.FullName = request.FullName;
            comment.IsConfirmed = false;


            //not : local ip her zaman 127.0.0.1
            string ip = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }

            comment.Ip = ip;
            BlogDBEntities db = new BlogDBEntities();
            db.Comment.Add(comment);
            db.SaveChanges();
            return RedirectToAction("ArticleDetail", new { articleId = request.Id  });
        }
    }
}