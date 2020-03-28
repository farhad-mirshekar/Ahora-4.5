using System;
namespace FM.Portal.Core.Model
{
   public class ListProductByCategoryIDVM
    {
        public Guid ProductID { get; set; }
        public string CategoryName { get; set; }
        public string FileName { get; set; }
        public string ProductName { get; set; }
    }
}
