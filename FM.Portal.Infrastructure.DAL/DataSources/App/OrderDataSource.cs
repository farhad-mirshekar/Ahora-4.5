using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class OrderDataSource : IOrderDataSource
    {
        public Result<Order> Get(GetOrderVM model)
        {
            try
            {
                Order obj = new Order();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                if (!model.ID.HasValue)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = model.ID;

                param[1] = new SqlParameter("@ShoppingID",SqlDbType.UniqueIdentifier);
                if (!model.ShoppingID.HasValue)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = model.ShoppingID;

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetOrder", param))
                    {
                        while (dr.Read())
                        {
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.SendType =(SendType) SQLHelper.CheckByteNull(dr["SendType"]);
                            obj.ShoppingID = SQLHelper.CheckGuidNull(dr["ShoppingID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.AddressID = SQLHelper.CheckGuidNull(dr["AddressID"]);
                            obj.BankID = SQLHelper.CheckGuidNull(dr["BankID"]);
                        }
                    }

                }
                return Result<Order>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Order>.Failure(message: e.ToString()); }
        }

        public Result Insert(Order model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@AddressID", model.AddressID);
                    param[2] = new SqlParameter("@BankID", model.BankID);
                    param[3] = new SqlParameter("@Price", model.Price);
                    param[4] = new SqlParameter("@SendType", (byte)model.SendType);
                    param[5] = new SqlParameter("@IsNewRecord", true);
                    param[6] = new SqlParameter("@ShoppingID", model.ShoppingID);
                    param[7] = new SqlParameter("@UserID", model.UserID);
                    param[8] = new SqlParameter("@TrackingCode", "");
                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyOrder", param);

                    return Result.Successful();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Order> Update(Order model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@AddressID", model.AddressID);
                    param[2] = new SqlParameter("@BankID", model.BankID);
                    param[3] = new SqlParameter("@Price", model.Price);
                    param[4] = new SqlParameter("@SendType", (byte)model.SendType);
                    param[5] = new SqlParameter("@IsNewRecord", false);
                    param[6] = new SqlParameter("@ShoppingID", model.ShoppingID);
                    param[7] = new SqlParameter("@UserID", model.UserID);
                    param[8] = new SqlParameter("@TrackingCode", model.TrackingCode);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyOrder", param);
                    return Get(new GetOrderVM { ID = model.ID, ShoppingID = null });
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
