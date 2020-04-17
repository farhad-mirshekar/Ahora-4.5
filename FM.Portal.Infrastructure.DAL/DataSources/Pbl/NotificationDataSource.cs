using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
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
    }
}
