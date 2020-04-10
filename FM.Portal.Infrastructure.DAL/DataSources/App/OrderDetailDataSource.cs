using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class OrderDetailDataSource : IOrderDetailDataSource
    {
        public Result<OrderDetail> Get(Guid ID)
        {
            try
            {
                var obj = new OrderDetail();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetOrderDetail", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.AttributeJson = SQLHelper.CheckStringNull(dr["AttributeJson"]);
                            obj.OrderID = SQLHelper.CheckGuidNull(dr["OrderID"]);
                            obj.ProductJson = SQLHelper.CheckStringNull(dr["ProductJson"]);
                            obj.UserJson = SQLHelper.CheckStringNull(dr["UserJson"]);
                        }
                    }
                }
                return Result<OrderDetail>.Successful(data: obj);
            }
            catch (Exception e) { return Result<OrderDetail>.Failure(); }
        }

        public Result Insert(OrderDetail model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@AttributeJson", model.AttributeJson);
                    param[2] = new SqlParameter("@OrderID", model.OrderID);
                    param[3] = new SqlParameter("@ProductJson", model.ProductJson);
                    param[4] = new SqlParameter("@UserJson", model.UserJson);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyOrderDetail", param);

                    return Result.Successful();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
