using FM.Portal.FrameWork.Attributes;
using System;
using DataAnnotations = System.ComponentModel.DataAnnotations;
namespace Ahora.WebApp.Models.App
{
    public class ProductCommentModel
    {
        [DataAnnotations.Required(ErrorMessage ="{0}")]
        [DisplayName("Product.Comment.Body")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
    }
}