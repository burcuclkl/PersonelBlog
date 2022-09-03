//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalBlog.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.ArticleImages = new HashSet<ArticleImages>();
            this.ArticleVideo = new HashSet<ArticleVideo>();
            this.Comment = new HashSet<Comment>();
            this.TagArticle = new HashSet<TagArticle>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string PreContent { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public Nullable<int> RateCount { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<decimal> RateValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleImages> ArticleImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleVideo> ArticleVideo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagArticle> TagArticle { get; set; }
    }
}
