using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

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

            var pageSize = PageSize.A4;
            var doc = new Document(new Rectangle(288f, 144f), 10, 10, 10, 10);
            doc.SetPageSize(pageSize.Rotate());
            var pdfWriter = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            var titleFont = GetFont();
            titleFont.SetStyle(Font.BOLD);
            titleFont.Color = BaseColor.BLACK;
            titleFont.Size = 14;
            var font = GetFont();
            var attributesFont = GetFont();
            attributesFont.SetStyle(Font.ITALIC);

            var orderCount = payment.Products.Count;
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
            creationDateCol.AddCell(new Paragraph($"{lng} : {Helper.GetPersianDate(payment.CreationDate)}", font));
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
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Name");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Count");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Price");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.Discount");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            lngResult = _localeStringResourceService.GetResource("PdfInvoice.Product.PriceSum");
            lng = lngResult.Data;
            productCellItem = new PdfPCell(new Phrase($"{lng}", font));
            productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            productTable.AddCell(productCellItem);

            foreach (var product in payment.Products)
            {
                productCellItem = new PdfPCell(new Phrase($"{orderNum + 1}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);

                if (product.Attributes.Count > 0)
                {
                    
                    productCellItem = new PdfPCell(new Phrase($"{product.Name}<br/>", font));
                    productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    productTable.AddCell(productCellItem);
                }
                else
                {
                    productCellItem = new PdfPCell(new Phrase($"{product.Name}", font));
                    productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    productTable.AddCell(productCellItem);
                }

                productCellItem = new PdfPCell(new Phrase($"{product.CountSelect}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);

                productCellItem = new PdfPCell(new Phrase(string.Format("{0:C0} تومان", product.Price).Replace("$", ""), font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);


                productCellItem = new PdfPCell(new Phrase($"{product.Name}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);

                productCellItem = new PdfPCell(new Phrase($"{product.Name}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);

                productCellItem = new PdfPCell(new Phrase($"{product.Name}", font));
                productCellItem.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                productTable.AddCell(productCellItem);
            }
            doc.Add(productTable);
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
        #endregion
    }
}
