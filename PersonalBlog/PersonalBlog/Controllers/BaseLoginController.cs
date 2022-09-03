using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Controllers
{
    public class BaseLoginController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[SessionKeyManager.LoginKey] == null)
            {
                filterContext.Result = new RedirectResult("/AdminLogin/Index");
            }
        }

    }
}