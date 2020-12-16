using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model.Ptl
{
    /// <summary>
    /// دسته بندی برای پرتال
    /// </summary>
   public class Category : Entity
    {
        /// <summary>
        /// نام دسته بندی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// آی دی پدر جهت زیر مجموعه
        /// </summary>
        public Guid ParentID { get; set; }
        public string Node { get; set; }
        public string ParentNode { get; set; }
        public Guid? RemoverID { get; set; }
        public DateTime? RemoverDate { get; set; }
        /// <summary>
        /// فقط برای نمایش دراپ دان
        /// </summary>
        public string TitleCrumb { get; set; }
    }
}
