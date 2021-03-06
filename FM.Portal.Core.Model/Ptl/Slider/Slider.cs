﻿using FM.Portal.BaseModel;

namespace FM.Portal.Core.Model
{
    /// <summary>
    /// تضاویر کشویی
    /// </summary>
   public class Slider : Entity
    {
        /// <summary>
        /// عنوان تصویر کشویی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// اولویت نمایش
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// نمایش : فعال یا غیر فعال
        /// </summary>
        public EnableMenuType Enabled { get; set; }
        /// <summary>
        /// لینک جهت کلیک روی تصویر
        /// </summary>
        public string Url { get; set; }

        //only show
        public int Total { get; set; }
        public string CreatorName { get; set; }
        public string Path => "slider";
    }
}
