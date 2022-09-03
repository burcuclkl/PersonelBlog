using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Data;

namespace PersonalBlog.Controllers
{
    public class AdminCategoryController : BaseLoginController
    {
       
        public ActionResult Index()
        {
            BlogDBEntities db = new BlogDBEntities();
            List<Category> allCategories = db.Category.ToList();
            ViewBag.Category = allCategories;

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddCategory(AddCategoryRequest request)
        {
            Category category = new Category();
            category.Name = request.Name;
            category.Description = request.Description;
            BlogDBEntities db = new BlogDBEntities();
            db.Category.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory()
        {
            int categoryId = Convert.ToInt32(Request.QueryString["id"]);
            BlogDBEntities db = new BlogDBEntities();
            Category deleteCategory = db.Category.Where(p => p.Id == categoryId).SingleOrDefault();
            db.Category.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update()
        {
            int categoryId = Convert.ToInt32(Request.QueryString["id"]);
            BlogDBEntities db = new BlogDBEntities();
            Category category = db.Category.Where(p => p.Id == categoryId).SingleOrDefault();
           
            return View(category);
        }

        public ActionResult UpdateCategory(UpdateCategoryRequest request)
        {
            BlogDBEntities db = new BlogDBEntities();
            Category updateCategory = db.Category.Where(p => p.Id == request.Id).SingleOrDefault();
            updateCategory.Name = request.Name;
            updateCategory.Description = request.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}