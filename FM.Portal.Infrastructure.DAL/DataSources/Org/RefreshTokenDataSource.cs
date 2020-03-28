using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class RefreshTokenDataSource : IRefreshTokenDataSource
    {
        public RefreshTokenDataSource()
        {

        }
        public Result<RefreshToken> Create(RefreshToken model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@ExpireDate", model.ExpireDate);
                    param[2] = new SqlParameter("@IssuedDate", model.IssuedDate);
                    param[3] = new SqlParameter("@ProtectedTicket", model.ProtectedTicket);
                    param[4] = new SqlParameter("@UserID", model.UserID);

                   var i= SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spCreateRefreshToken", param);
                        return Get(model.ID);
                }
            }
            catch (Exception e) { return Result<RefreshToken>.Failure(); }
        }

        public Result Delete(Guid ID)
        {
           try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spDeleteRefreshToken", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e)
            {
                return Result.Failure();
            }
        }

        public Result<RefreshToken> Get(Guid ID)
        {
            try
            {
                RefreshToken obj = new RefreshToken();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetRefreshToken", param))
                    {
                        while (dr.Read())
                        {
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.ExpireDate = SQLHelper.CheckDateTimeNull(dr["ExpireDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.IssuedDate = SQLHelper.CheckDateTimeNull(dr["IssuedDate"]);
                            obj.ProtectedTicket = SQLHelper.CheckStringNull(dr["ProtectedTicket"]);
                        }
                    }

                }
                return Result<RefreshToken>.Successful(data: obj);
            }
            catch (Exception e) { return Result<RefreshToken>.Failure(); }
        }
    }
}
