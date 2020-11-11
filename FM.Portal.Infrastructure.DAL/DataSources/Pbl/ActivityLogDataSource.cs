using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class ActivityLogDataSource : IActivityLogDataSource
    {
        public Result<ActivityLog> Insert(ActivityLog model)
        => Modify(true, model);

        public Result Delete(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Result<ActivityLog> Update(ActivityLog model)
        {
            throw new NotImplementedException();
        }

        public Result<ActivityLog> Get(Guid ID)
        {
            try
            {
                var obj = new ActivityLog();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetActivityLog", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.ActivityLogTypeID = SQLHelper.CheckGuidNull(dr["ActivityLogTypeID"]);
                            obj.Comment = SQLHelper.CheckStringNull(dr["Comment"]);
                            obj.EntityID = SQLHelper.CheckGuidNull(dr["EntityID"]);
                            obj.EntityName = SQLHelper.CheckStringNull(dr["EntityName"]);
                            obj.IpAddress= SQLHelper.CheckStringNull(dr["IpAddress"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreatorName = SQLHelper.CheckStringNull(dr["CreatorName"]);
                        }
                    }

                }
                return Result<ActivityLog>.Successful(data: obj);
            }
            catch (Exception e) { return Result<ActivityLog>.Failure(); }
        }

        public DataTable List(ActivityLogListVM listVM)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@PageSize", listVM.PageSize);
                param[1] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsActivityLog", param);
            }
            catch (Exception e) { throw; }
        }

        private Result<ActivityLog> Modify(bool IsNewrecord, ActivityLog model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@ActivityLogTypeID", model.ActivityLogTypeID);
                    param[3] = new SqlParameter("@Comment", model.Comment);
                    param[4] = new SqlParameter("@UserID", model.UserID);
                    param[5] = new SqlParameter("@IpAddress", model.IpAddress);
                    param[6] = new SqlParameter("@EntityID", model.EntityID);
                    param[7] = new SqlParameter("@EntityName", model.EntityName);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyActivityLog", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<ActivityLog>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
