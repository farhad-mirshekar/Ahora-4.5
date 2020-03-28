using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;

namespace FM.Portal.Infrastructure.DAL
{
    public class ShoppingCartItemDataSource : IShoppingCartItemDataSource
    {

        private DataTable Modify(bool IsNewRecord, ShoppingCartItem model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@AttributeJson", model.AttributeJson);
                    param[1] = new SqlParameter("@ProductID", model.ProductID);
                    param[2] = new SqlParameter("@Quantity", model.Quantity);
                    param[3] = new SqlParameter("@ShoppingID", model.ShoppingID);
                    param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[5] = new SqlParameter("@UserID", model.UserID);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyShoppingCartItem", param);

                    return List(model.ShoppingID);
                }
            }
            catch { throw; }
        }
        public DataTable Insert(ShoppingCartItem model)
        {
            return Modify(true, model);
        }

        public DataTable List(Guid ShoppingID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ShoppingID", ShoppingID);
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsShoppingCartItem", param);
        }

        public DataTable Update(ShoppingCartItem model)
        {
            return Modify(false, model);
        }

        public DataTable Delete(DeleteCartItemVM model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ShoppingID", model.ShoppingID);
                param[1] = new SqlParameter("@ProductID", model.ProductID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spDeleteShoppingCartItem", param);
            }
            catch { throw; }
        }

        public Result<ShoppingCartItem> Get(Guid ShoppingID, Guid ProductID)
        {
            try
            {
                ShoppingCartItem obj = new ShoppingCartItem();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ShoppingID", ShoppingID);
                param[1] = new SqlParameter("@ProductID", ProductID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetShoppingCartItemByProductID", param))
                    {
                        while (dr.Read())
                        {
                            obj.ProductID = SQLHelper.CheckGuidNull(dr["ProductID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Quantity = SQLHelper.CheckIntNull(dr["Quantity"]);
                            obj.ShoppingID = SQLHelper.CheckGuidNull(dr["ShoppingID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.AttributeJson = SQLHelper.CheckStringNull(dr["AttributeJson"]);
                        }
                    }

                }
                return Result<ShoppingCartItem>.Successful(data: obj);
            }

            catch (Exception e) { return Result<ShoppingCartItem>.Failure(); }
        }
    }
}
