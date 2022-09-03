using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.Models
{
    public class AddImageRequest
    {
        public HttpPostedFileBase[] Images { get; set; }

        public int ArticleId { get; set; }
    }
}