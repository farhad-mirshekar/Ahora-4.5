using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class NotificationDataSource : INotificationDataSource
    {
        readonly IRequestInfo _requestInfo;
        public NotificationDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        public DataTable List()
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserID", _requestInfo.UserId);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsNotification", param);
            }
            catch (Exception e) { throw; }
        }

        public Result ReadNotification(Guid ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@ID", ID);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, param, "pbl.spReadNotification");
                    return Result.Successful();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
