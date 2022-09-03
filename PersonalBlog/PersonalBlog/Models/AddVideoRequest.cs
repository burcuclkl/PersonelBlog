using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Models
{
    public class AddVideoRequest
    {
        [AllowHtml]
        public string Frame { get; set; }

        public string Description { get; set; }

        public int ArticleId { get; set; }
    }
}