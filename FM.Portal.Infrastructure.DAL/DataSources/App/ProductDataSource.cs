using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Data.SqlClient;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductDataSource : IProductDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public ProductDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        private Result<Product> Modify(bool isNewRecord , Product model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[24];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Name", model.Name);
                    param[2] = new SqlParameter("@ShortDescription", model.ShortDescription);
                    param[3] = new SqlParameter("@FullDescription", model.FullDescription);
                    param[4] = new SqlParameter("@UserID", _requestInfo.UserId);
                    param[5] = new SqlParameter("@isNewRecord", isNewRecord);
                    param[6] = new SqlParameter("@CallForPrice", model.CallForPrice);

                    param[7] = new SqlParameter("@AllowCustomerReviews", model.AllowCustomerReviews);
                    param[8] = new SqlParameter("@Height", model.Height);
                    param[9] = new SqlParameter("@Weight", model.Weight);
                    param[10] = new SqlParameter("@Width", model.Width);
                    param[11] = new SqlParameter("@Length", model.Length);
                    param[12] = new SqlParameter("@MetaDescription", model.MetaDescription);

                    param[13] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                    param[14] = new SqlParameter("@MetaTitle", model.MetaTitle);
                    param[15] = new SqlParameter("@Discount", model.Discount);
                    param[16] = new SqlParameter("@SpecialOffer", model.SpecialOffer);
                    param[17] = new SqlParameter("@Price", model.Price);
                    param[18] = new SqlParameter("@Published", model.Published);
                    param[19] = new SqlParameter("@CategoryID", model.CategoryID);
                    param[20] = new SqlParameter("@ShowOnHomePage", model.ShowOnHomePage);
                    param[21] = new SqlParameter("@StockQuantity", model.StockQuantity);
                    param[22] = new SqlParameter("@DiscountType", (byte)model.DiscountType);
                    param[23] = new SqlParameter("@HasDiscount", model.HasDiscount);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProduct", param);

                    return Get(model.ID,null);
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<Product> Get(Guid? ID,string TrackingCode)
        {
            try
            {
                Product obj = new Product();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                if (ID.HasValue)
                    param[0].Value = ID;
                else
                    param[0].Value = DBNull.Value;

                param[1] = new SqlParameter("@TrackingCode", SqlDbType.NVarChar);
                if (TrackingCode =="" || TrackingCode == null)
                    param[1].Value = DBNull.Value;
                
                else
                    param[1].Value = TrackingCode;

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProduct", param))
                    {
                        while (dr.Read())
                        {
                            obj.AllowCustomerReviews = SQLHelper.CheckBoolNull(dr["AllowCustomerReviews"]);
                            obj.ApproveDRatingSum = SQLHelper.CheckIntNull(dr["ApproveDRatingSum"]);
                            obj.ApprovedTotalReviews = SQLHelper.CheckIntNull(dr["ApprovedTotalReviews"]);
                            obj.CallForPrice = SQLHelper.CheckBoolNull(dr["CallForPrice"]);
                            obj.CategoryID = SQLHelper.CheckGuidNull(dr["CategoryID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.FullDescription = SQLHelper.CheckStringNull(dr["FullDescription"]);
                            obj.Height = SQLHelper.CheckDecimalNull(dr["Height"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Length = SQLHelper.CheckDecimalNull(dr["Length"]);
                            obj.MetaDescription = SQLHelper.CheckStringNull(dr["MetaDescription"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.MetaTitle = SQLHelper.CheckStringNull(dr["MetaTitle"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.NotApprovedRatingSum = SQLHelper.CheckIntNull(dr["NotApprovedRatingSum"]);
                            obj.NotApprovedTotalReviews = SQLHelper.CheckIntNull(dr["NotApprovedTotalReviews"]);
                            obj.Discount = SQLHelper.CheckDecimalNull(dr["Discount"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.Published = SQLHelper.CheckBoolNull(dr["Published"]);
                            obj.ShortDescription = SQLHelper.CheckStringNull(dr["ShortDescription"]);
                            obj.ShowOnHomePage = SQLHelper.CheckBoolNull(dr["ShowOnHomePage"]);
                            obj.SpecialOffer = SQLHelper.CheckBoolNull(dr["SpecialOffer"]);
                            obj.UpdatedDate = SQLHelper.CheckDateTimeNull(dr["UpdatedDate"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.Weight = SQLHelper.CheckDecimalNull(dr["Weight"]);
                            obj.Width = SQLHelper.CheckDecimalNull(dr["Width"]);
                            obj.StockQuantity = SQLHelper.CheckIntNull(dr["StockQuantity"]);
                            obj.DiscountName = SQLHelper.CheckStringNull(dr["DiscountName"]);
                            obj.DiscountAmount = SQLHelper.CheckDecimalNull(dr["DiscountAmount"]);
                            obj.HasDiscountsApplied = SQLHelper.CheckBoolNull(dr["HasDiscountsApplied"]);
                            obj.DiscountType =(DiscountType) SQLHelper.CheckByteNull(dr["DiscountType"]);
                            obj.HasDiscount = SQLHelper.CheckBoolNull(dr["HasDiscount"]);
                        }
                    }

                }
                return Result<Product>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Product>.Failure(); }
        }

        public Result<Product> Insert(Product model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProduct", null);
            }
            catch (Exception e) { throw; }
        }

        public Result<Product> Update(Product model)
        {
            return Modify(false, model);
        }

        public DataTable ListAttributeForProduct(Guid ProductID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ProductID", ProductID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spListAttributeForProduct", param);
            }
            catch (Exception e) { throw; }
        }

        public DataTable ListProductVarientAttribute(Guid AttributeID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@AttributeID", AttributeID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spListVarientForProduct", param);
            }
            catch (Exception e) { throw; }
        }

        public DataTable ListProductShowOnHomePage(int Count)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Count", Count);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spListProductShowOnHomePage", param);
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(Guid CategoryID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@CategoryID", CategoryID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductByCategoryID", param);
            }
            catch (Exception e) { throw; }
        }
    }
}
