using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class ActivityLogTypeDataSource : IActivityLogTypeDataSource
    {
        public Result<ActivityLogType> Insert(ActivityLogType model)
        => Modify(true, model);

        public Result Delete(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Result<ActivityLogType> Update(ActivityLogType model)
        {
            throw new NotImplementedException();
        }

        public Result<ActivityLogType> Get(Guid ID)
        {
            try
            {
                var obj = new ActivityLogType();
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
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.SystemKeyword = SQLHelper.CheckStringNull(dr["SystemKeyword"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
  
                        }
                    }

                }
                return Result<ActivityLogType>.Successful(data: obj);
            }
            catch (Exception e) { return Result<ActivityLogType>.Failure(); }
        }

        public DataTable List(ActivityLogTypeListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@PageSize", listVM.PageSize);
                param[1] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[2] = new SqlParameter("@SystemKeyword", listVM.SystemKeyword);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsActivityLogType", param);
            }
            catch (Exception e) { throw; }
        }

        private Result<ActivityLogType> Modify(bool IsNewrecord, ActivityLogType model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@SystemKeyword", model.SystemKeyword);
                    param[4] = new SqlParameter("@Enabled", (byte)model.Enabled);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyActivityLogType", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<ActivityLogType>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
