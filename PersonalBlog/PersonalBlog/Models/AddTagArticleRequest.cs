using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.Models
{
    public class AddTagArticleRequest
    {

        public int ArticleId { get; set; }
        public int TagId { get; set; }
    }
}