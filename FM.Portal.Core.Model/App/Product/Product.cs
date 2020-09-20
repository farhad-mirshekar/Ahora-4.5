﻿using FM.Portal.BaseModel;
using FM.Portal.Core.Common;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Model
{
   public class Product:Entity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public int ApproveDRatingSum { get; set; }
        public int NotApprovedRatingSum { get; set; }
        public int ApprovedTotalReviews { get; set; }
        public int NotApprovedTotalReviews { get; set; }
        public bool CallForPrice { get; set; }
        public decimal Price { get; set; }
        //public decimal OldPrice { get; set; }
        public bool  SpecialOffer { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public bool Published { get; set; }
        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string TrackingCode { get; set; }
        public int StockQuantity { get; set; }
        public bool HasDiscount { get; set; }
        public bool IsDownload { get; set; }
        public Guid? ShippingCostID { get; set; }
        public Guid? DeliveryDateID { get; set; }

        //only show
        public string DiscountName { get; set; }
        public DiscountType DiscountTypes { get; set; }
        public decimal DiscountAmount { get; set; }
        public int CountSelect { get; set; }
        public string CreationDatePersian => Helper.GetPersianDate(CreationDate);
        public string CategoryName { get; set; }
        public List<RelatedProduct> RelatedProducts { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public DeliveryDate DeliveryDate { get; set; }
        public Category Category { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<ListAttributeForSelectCustomerVM> Attributes { get; set; }
        public int Total { get; set; }
    }
}
