using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.Models
{
    public class AddCategoryRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}