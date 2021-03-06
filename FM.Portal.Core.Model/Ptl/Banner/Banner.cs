﻿using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;

namespace FM.Portal.Core.Model
{
   public class Banner:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EnableMenuType Enabled { get; set; }
        public BannerType BannerType { get; set; }
        public Guid UserID { get; set; }

        //only show
        public string FileName { get; set; }
        public PathType PathType { get; set; }
        public string Path => PathType.ToString();
        public int Total { get; set; }
    }
}
