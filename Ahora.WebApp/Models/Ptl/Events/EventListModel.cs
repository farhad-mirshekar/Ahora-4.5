using @Model = FM.Portal.Core.Model;

using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl.Events
{
    public class EventListModel
    {
        public List<Model.Events> AvailableEvents { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}