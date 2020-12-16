using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class EventListModel
    {
        public EventListModel()
        {
            AvailableEvents = new List<EventModel>();
        }
        public List<EventModel> AvailableEvents { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}