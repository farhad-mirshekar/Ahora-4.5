using FM.Portal.FrameWork.Attributes;
using System;
namespace Ahora.WebApp.Models.App
{
    public class ProductCommentModel
    {
        [Required("Product.Comment.Body.ErrorMessage")]
        [DisplayName("Product.Comment.Body")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
    }
}