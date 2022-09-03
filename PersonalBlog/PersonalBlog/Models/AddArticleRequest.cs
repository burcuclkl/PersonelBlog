using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Models
{
    public class AddArticleRequest
    {
        [AllowHtml]
        public string Title { get; set; }


        [AllowHtml]
        public string PreContent  { get; set; }

        [AllowHtml]
        public string Content { get; set; }


        public int CategoryId { get; set; }


    }
}