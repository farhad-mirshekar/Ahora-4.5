using FM.Portal.FrameWork.Attributes;
using System;

namespace Ahora.WebApp.Models.Ptl
{
    public class NewsCommentModel
    {
        [Required("News.Comment.Body.ErrorMessage")]
        [DisplayName("News.Comment.Body")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid NewsID { get; set; }
    }
}