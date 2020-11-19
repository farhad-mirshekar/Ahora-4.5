using iTextSharp.text;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.App
{
    public class ProductListModel
    {
        public List<ProductModel> AvailableProducts  { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}