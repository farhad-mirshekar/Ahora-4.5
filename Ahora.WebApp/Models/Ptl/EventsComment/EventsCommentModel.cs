using FM.Portal.FrameWork.Attributes;
using System;
namespace Ahora.WebApp.Models.Ptl
{
    public class EventsCommentModel
    {
        [Required("Events.Comment.Body.ErrorMessage")]
        [DisplayName("Events.Comment.Body")]
        public string Body { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid UserID { get; set; }
        public Guid EventsID { get; set; }
    }
}