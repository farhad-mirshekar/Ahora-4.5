using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class OrderDataSource : IOrderDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public OrderDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        public Result<Order> Get(Guid ID)
        {
            try
            {
                Order obj = new Order();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetOrder", param))
                    {
                        while (dr.Read())
                        {
                            obj.TrackingCode = SQLHelper.CheckIntNull(dr["TrackingCode"]);
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
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@AddressID", model.AddressID);
                    param[2] = new SqlParameter("@BankID", model.BankID);
                    param[3] = new SqlParameter("@Price", model.Price);
                    param[4] = new SqlParameter("@SendType", (byte)model.SendType);
                    //param[5] = new SqlParameter("@isNewRecord", isNewrecord);
                    param[5] = new SqlParameter("@ShoppingID", model.ShoppingID);
                    param[6] = new SqlParameter("@UserID", model.UserID);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyOrder", param);

                    return Result.Successful();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
