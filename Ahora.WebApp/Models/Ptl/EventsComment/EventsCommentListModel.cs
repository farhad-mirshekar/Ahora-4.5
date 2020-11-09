﻿using @Model= FM.Portal.Core.Model;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace Ahora.WebApp.Models.Ptl.EventsComment
{
    public class EventsCommentListModel
    {
        public List<Model.EventsComment> AvailableComments { get; set; }
        public User User { get; set; }
        public Model.Events Events { get; set; }
    }
}