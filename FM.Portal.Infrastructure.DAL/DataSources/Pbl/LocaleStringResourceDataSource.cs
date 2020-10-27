using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class LocaleStringResourceDataSource : ILocaleStringResourceDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public LocaleStringResourceDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteLocaleStringResource", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<LocaleStringResource> Get(Guid ID)
        {
            try
            {
                var obj = new LocaleStringResource();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetLocaleStringResource", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.ResourceName = SQLHelper.CheckStringNull(dr["ResourceName"]);
                            obj.ResourceValue = SQLHelper.CheckStringNull(dr["ResourceValue"]);
                            obj.LanguageID = SQLHelper.CheckGuidNull(dr["LanguageID"]);
                        }
                    }

                }
                return Result<LocaleStringResource>.Successful(data: obj);
            }
            catch (Exception e) { return Result<LocaleStringResource>.Failure(); }
        }

        public Result<LocaleStringResource> Insert(LocaleStringResource model)
        => Modify(true, model);

        public DataTable List(LocaleStringResourceListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@LanguageID", listVM.LanguageID);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsLocaleStringResource", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<LocaleStringResource> Update(LocaleStringResource model)
        => Modify(false, model);

        private Result<LocaleStringResource> Modify(bool IsNewrecord, LocaleStringResource model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@ResourceName", model.ResourceName);
                    param[3] = new SqlParameter("@ResourceValue", model.ResourceValue);
                    param[4] = new SqlParameter("@LanguageID", model.LanguageID);
                    param[5] = new SqlParameter("@UserId", _requestInfo.UserId);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyLocaleStringResource", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<LocaleStringResource>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
