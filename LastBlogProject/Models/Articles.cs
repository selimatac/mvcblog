//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LastBlogProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Articles
    {
        public Articles()
        {
            this.Comments = new HashSet<Comments>();
        }

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public Nullable<int> AuthorId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string FeaturedImage { get; set; }
        public string PostContent { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> CommentId { get; set; }

        public virtual Authors Authors { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Comments Comments1 { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
