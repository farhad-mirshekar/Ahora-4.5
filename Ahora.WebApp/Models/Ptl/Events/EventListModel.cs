using @Model = FM.Portal.Core.Model;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.Ptl
{
    public class EventListModel
    {
        public List<Model.Events> AvailableEvents { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}