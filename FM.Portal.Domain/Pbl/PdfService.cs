using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FM.Portal.Domain
{
    public class PdfService : IPdfService
    {
        private readonly IPaymentService _paymentService;
        private readonly ILanguageService _languageService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IWorkContext _workContext;
        private readonly IOrderService _orderService;

        public PdfService(IPaymentService paymentService
                          , ILanguageService languageService
                          , IWorkContext workContext
                          , ILocaleStringResourceService localeStringResourceService
                          , IOrderService orderService)
        {
            _paymentService = paymentService;
            _languageService = languageService;
            _workContext = workContext;
            _localeStringResourceService = localeStringResourceService;
            _orderService = orderService;
        }
        public Result<string> PrintPaymentToPdf(Guid PaymentID, Guid LanguageID)
        {
            try
            {
                var paymentDetailResult = _paymentService.GetDetail(PaymentID);
                if (!paymentDetailResult.Success)
                    return Result<string>.Failure(message: paymentDetailResult.Message);
                var paymentDetail = paymentDetailResult.Data;

                var orderResult = _orderService.Get(new GetOrderVM() { ID = paymentDetail.Payment.OrderID });
                if (!orderResult.Success)
                    return Result<string>.Failure(message: orderResult.Message);
                var order = orderResult.Data;

                paymentDetail.Order = order;
                var trackingCode = order.TrackingCode.Split('-');
                string fileName = string.Format("order_{0}.pdf", trackingCode[1].ToString());
                string filePath = Path.Combine(FileHelper.MapPath("~/files/Pdf"), fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PrintToPdf(fileStream, paymentDetail, LanguageID);
                }
                return Result<string>.Successful(data: filePath);

            }
            catch (Exception e) { throw; }
        }

        #region Private - Utilities
        private void PrintToPdf(Stream stream, PaymentDetailVM payment, Guid LanguageID)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (payment == null)
                throw new ArgumentNullException("payment detail");
            
            var orderCount = payment.Products.Count;
            var pageSize = PageSize.LETTER;
            if (orderCount > 5)
            {
                pageSize = PageSize.A4;
            }
            var doc = new Document(pageSize);
            doc.SetPageSize(pageSize.Rotate());
            var pdfWriter = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            var titleFont = GetFont();
            titleFont.SetStyle(Font.BOLD);
            titleFont.Color = BaseColor.BLACK;
            titleFont.Size = 14;
            var font = GetFont();
            font.Size = 12;
            var attributesFont = GetFont();
            attributesFont.SetStyle(Font.ITALIC);

            var orderNum = 0;

            var workingLanguage = _workContext.WorkingLanguage;
            var trackingCode = payment.Order.TrackingCode.Split('-');

            var languageResult = _languageService.Get(LanguageID);
            if (languageResult.Success)
                workingLanguage = languageResult.Data;

            #region Header
            //header
            PdfPTable headerTable = new PdfPTable(3);
            headerTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            headerTable.DefaultCell.Border = Rectangle.BOX;
            headerTable.WidthPercentage = 100f;


            var lngResult = _localeStringResourceService.GetResource("PdfInvoice.TrackingCode");
            var lng = lngResult.Data;

            var trackingCodeCol = new PdfPTable(1);
            SetDefaultCell(trackingCodeCol);
            trackingCodeCol.AddCell(new Paragraph($"{ lng } : { trackingCode[1]}", font));
            headerTable.AddCell(trackingCodeCol);

            var siteNameCol = new PdfPTable(1);
            SetDefaultCell(siteNameCol);
            siteNameCol.AddCell(new Paragraph($"{Helper.SiteName}", font));
            headerTable.AddCell(siteNameCol);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.CreationDate");
            lng = lngResult.Data;

            var creationDateCol = new PdfPTable(1);
            SetDefaultCell(creationDateCol);
            creationDateCol.AddCell(new Paragraph($"{lng} : {Helper.GetPersianDate(payment.CreationDate.Value)}", font));
            headerTable.AddCell(creationDateCol);

            doc.Add(headerTable);
            doc.Add(new Paragraph(" "));
            #endregion

            #region User
            var userTable = new PdfPTable(3);
            userTable.WidthPercentage = 100f;
            userTable.DefaultCell.Border = Rectangle.BOX;

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.Title");
            lng = lngResult.Data;
            var header = new PdfPCell(new Phrase($"{lng}", titleFont));
            header.Colspan = 3;
            header.Padding = 6;
            header.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            userTable.AddCell(header);

            var userInfo = new PdfPTable(1);
            SetDefaultCell(userInfo);
            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.Email");
            lng = lngResult.Data;
            userInfo.AddCell(new Paragraph($"{lng} : {payment.User.Email}", font));
            userTable.AddCell(userInfo);

            userInfo = new PdfPTable(1);
            SetDefaultCell(userInfo);
            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.CellPhone");
            lng = lngResult.Data;
            userInfo.AddCell(new Paragraph($"{lng} : {payment.User.CellPhone}", font));
            userTable.AddCell(userInfo);

            userInfo = new PdfPTable(1);
            SetDefaultCell(userInfo);
            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.NameFamily");
            lng = lngResult.Data;

            userInfo.AddCell(new Paragraph($"{lng} : {payment.User.FirstName + payment.User.LastName}", font));
            userTable.AddCell(userInfo);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.Address");
            lng = lngResult.Data;
            var addressUser = new PdfPCell(new Phrase($"{lng} : {payment.UserAddress.Address}", font));
            addressUser.Colspan = 3;
            addressUser.Padding = 5;
            addressUser.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            userTable.AddCell(addressUser);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.User.PostalCode");
            lng = lngResult.Data;
            var postalCodeUser = new PdfPCell(new Phrase($"{lng} : {payment.UserAddress.PostalCode}", font));
            postalCodeUser.Colspan = 3;
            postalCodeUser.Padding = 5;
            postalCodeUser.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            userTable.AddCell(postalCodeUser);

            doc.Add(userTable);
            doc.Add(new Paragraph(" "));
            #endregion

            #region Product
            var productTable = new PdfPTable(6);
            productTable.WidthPercentage = 100f;
            productTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.DefaultCell.Border = Rectangle.BOX;
            productTable.SetWidths(new[] { 15,15,15,15,35,5});

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Title");
            lng = lngResult.Data;
            var headerProduct = new PdfPCell(new Phrase($"{lng}", titleFont));
            headerProduct.Colspan = 6;
            headerProduct.Padding = 6;
            headerProduct.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(headerProduct);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.IndexRow");
            lng = lngResult.Data;
            var productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productCellItem.Padding = 6;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Name");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.Padding = 6;
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Count");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.Padding = 6;
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Price");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.Padding = 6;
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Discount");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.Padding = 6;
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.PriceSum");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.Padding = 6;
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);
            decimal totalSum = 0;
            foreach (var product in payment.Products)
            {
                productCellItem = new PdfPCell(new Phrase($"{++orderNum}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productCellItem.Padding = 6;
                productTable.AddCell(productCellItem);

                var attributes = "";
                decimal attributePrice = 0;
                foreach (var userSelectedAttribute in payment.Attributes)
                {
                    foreach (var forSelectCustomerVM in product.Attributes)
                    {
                        var attribute = forSelectCustomerVM.ProductVariantAttributeValue.Where(a => a.ID == userSelectedAttribute.ID).FirstOrDefault();
                        if (attribute != null)
                        {
                            if (userSelectedAttribute.Price > 0)
                            {
                                attributePrice = userSelectedAttribute.Price;
                                attributes += $"{userSelectedAttribute.AttributeName}:{userSelectedAttribute.Name} - {GetMoney(userSelectedAttribute.Price)} ";
                            }
                            else
                               attributes += $" {userSelectedAttribute.AttributeName}:{userSelectedAttribute.Name} ";
                        }
                    }
                }
                productCellItem = new PdfPCell(new Phrase($"{product.Name} - {attributes}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productCellItem.Padding = 6;
                productTable.AddCell(productCellItem);

                productCellItem = new PdfPCell(new Phrase($"{product.CountSelect}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productCellItem.Padding = 6;
                productTable.AddCell(productCellItem);

                productCellItem = new PdfPCell(new Phrase(GetMoney(product.Price), font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productCellItem.Padding = 6;
                productTable.AddCell(productCellItem);

                decimal temp1 = 0, temp2 = 0;
                var categoryDiscountName = "";
                if (product.Category.HasDiscountsApplied)
                {
                    categoryDiscountName = product.DiscountName;
                    switch (product.DiscountType)
                    {
                        case DiscountType.درصدی:
                            temp2 = (product.Price * product.DiscountAmount) / 100;
                            break;
                        case DiscountType.مبلغی:
                            temp2 = product.DiscountAmount;
                            break;
                    }
                }

                if (product.HasDiscount)
                {
                    switch (product.DiscountTypes)
                    {
                        case DiscountType.درصدی:
                            temp1 = (product.Price * product.Discount) / 100;
                            break;
                        case DiscountType.مبلغی:
                            temp1 = product.Discount;
                            break;
                    }
                }
                productCellItem = new PdfPCell(new Phrase(string.Format("{0:C0}, {1}:{2:C0}",GetMoney(temp1),categoryDiscountName,GetMoney(temp2)), font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productCellItem.Padding = 6;
                productTable.AddCell(productCellItem);

               var amountBasket = attributePrice + (product.Price - (temp1 + temp2)) * product.CountSelect;
                totalSum += amountBasket;
                productCellItem = new PdfPCell(new Phrase(GetMoney(amountBasket), font));
                productCellItem.Padding = 6;
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);

            }


            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.TotalSum");
            lng = lngResult.Data;
            var footerProduct = new PdfPCell(new Phrase($"{lng}", titleFont));
            footerProduct.Colspan = 5;
            footerProduct.Padding = 6;
            footerProduct.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            footerProduct.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            productTable.AddCell(footerProduct);

            productCellItem = new PdfPCell(new Phrase(GetMoney(totalSum), font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            doc.Add(productTable);
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));
            #endregion

            #region Store
            var storTable = new PdfPTable(2);
            storTable.WidthPercentage = 100f;
            storTable.DefaultCell.Border = Rectangle.BOX;
            storTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Store.Title");
            lng = lngResult.Data;
            var headerStore = new PdfPCell(new Phrase($"{lng}", titleFont));
            headerStore.Colspan = 2;
            headerStore.Padding = 6;
            headerStore.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            storTable.AddCell(headerStore);

            var storInfo = new PdfPTable(1);
            SetDefaultCell(storInfo);
            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Store.Address");
            lng = lngResult.Data;
            storInfo.AddCell(new Paragraph($"{lng} : {Helper.Address}", font));
            storTable.AddCell(storInfo);

            storInfo = new PdfPTable(1);
            SetDefaultCell(storInfo);
            storInfo.AddCell(new Paragraph($"محل مهر یا امضای فروشنده", font));
            storTable.AddCell(storInfo);

            doc.Add(storTable);
            #endregion

            doc.Close();

        }

        protected virtual Font GetFont()
        {
            //nopCommerce supports unicode characters
            //nopCommerce uses Free Serif font by default (~/App_Data/Pdf/FreeSerif.ttf file)
            //It was downloaded from http://savannah.gnu.org/projects/freefont
            string fontPath = Path.Combine(FileHelper.MapPath("~/App_Data/Pdf/"), "IRANSansWeb.ttf");
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 10, Font.NORMAL);
            return font;
        }
        private void SetDefaultCell(PdfPTable pdfPTable)
        {
            pdfPTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            pdfPTable.DefaultCell.Border = Rectangle.NO_BORDER;
            pdfPTable.DefaultCell.Padding = 5;
        }
        private string GetMoney(decimal price)
        {
            return string.Format("{0:C0}", price).Replace("$", "");
        }
        #endregion
    }
}
