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
    
    public partial class Comments
    {
        public Comments()
        {
            this.Articles1 = new HashSet<Articles>();
        }
    
        public int CommentID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> AuthorId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int ArticleId { get; set; }
    
        public virtual Articles Articles { get; set; }
        public virtual Authors Authors { get; set; }
        public virtual ICollection<Articles> Articles1 { get; set; }
    }
}