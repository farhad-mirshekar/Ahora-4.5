using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl
{
    public class EventsCommentListModel
    {
        public EventsCommentListModel()
        {
            AvailableComments = new List<EventsComment>();
            Events = new Events();
        }
        public List<Model.EventsComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.Events Events { get; set; }
    }
}