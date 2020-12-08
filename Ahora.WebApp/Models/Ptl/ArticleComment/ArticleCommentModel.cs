using FM.Portal.FrameWork.Attributes;
using System;

namespace Ahora.WebApp.Models.Ptl
{
    public class ArticleCommentModel
    {
        [Required("Article.Comment.Body.ErrorMessage")]
        [DisplayName("Article.Comment.Body")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ArticleID { get; set; }
    }
}