﻿using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Menu : Entity
    {
        public string Name { get; set; }
        public EnableMenuType Enabled { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public Guid ParentID { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; }
        public int Priority { get; set; }
        public string Parameters { get; set; }
        public EnableMenuType ForeignLink { get; set; }
        public Guid RemoverID { get; set; }
        public DateTime RemoverDate { get; set; }

        //only show
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
    }
}
