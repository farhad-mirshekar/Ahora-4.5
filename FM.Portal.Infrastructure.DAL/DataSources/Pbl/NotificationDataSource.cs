﻿using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
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

        public Result<Notification> Get(Guid ID)
        {
            try
            {
                var obj = new Notification();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetNotification", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ReadDate = SQLHelper.CheckDateTimeNull(dr["ReadDate"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.PositionID = SQLHelper.CheckGuidNull(dr["PositionID"]);
                        }
                    }

                }
                return Result<Notification>.Successful(data: obj);
            }
            catch
            {
                return Result<Notification>.Failure();
            }
        }

        public DataTable GetActiveNotification()
        {
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@PositionID", _requestInfo.PositionId);
                    param[1] = new SqlParameter("@UserID", _requestInfo.UserId);

                    return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsActiveNotification", param);
                }
                catch (Exception e) { throw; }
            }
        }

        public DataTable List(NotificationListVM listVM)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@UserID", _requestInfo.UserId);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

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
