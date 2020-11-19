using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace Ahora.WebApp.Models.App
{
    public class ShoppingCartItemListModel
    {
        public List<ShoppingCartItemModel> AvailableShoppingCartItem { get; set; }
        public Decimal AmountBasket { get; set; }
        public List<UserAddress> UserAddress { get; set; }
    }
    public class ShoppingCartItemModel : EntityModel
    {
        public ShoppingCartItemModel()
        {
            AttributeModel = new List<AttributeModel>();
        }
        public Guid ShoppingID { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Attachment PictureAttachment { get; set; }
        public int Quantity { get; set; }
        public List<AttributeModel> AttributeModel { get; set; }
        public Category Category { get; set; }
        public Discount CategoryDiscount { get; set; }
        public ShippingCost ShippingCost { get; set; }
        public DeliveryDate DeliveryDate { get; set; }
        public string Path => "Product";
    }

    #region Nested Clasess
    public class AttributeModel
    {
        public Guid ID { get; set; }
        public Guid ProductVariantAttributeID { get; set; }
        public string AttributeName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid ProductID { get; set; }
    }
    #endregion

}