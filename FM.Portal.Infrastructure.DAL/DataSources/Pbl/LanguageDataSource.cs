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
    public class LanguageDataSource : ILanguageDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public LanguageDataSource(IRequestInfo requestInfo)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteLanguage", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Language> Get(Guid ID)
        {
            try
            {
                var obj = new Language();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetLanguage", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.LanguageCultureType = (LanguageCultureType)SQLHelper.CheckByteNull(dr["LanguageCultureType"]);
                            obj.UniqueSeoCode = SQLHelper.CheckStringNull(dr["UniqueSeoCode"]);
                        }
                    }

                }
                return Result<Language>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Language>.Failure(); }
        }

        public Result<Language> Insert(Language model)
        => Modify(true, model);

        public DataTable List(LanguageListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@Name", listVM?.Name);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsLanguage", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Language> Update(Language model)
        => Modify(false, model);
        private Result<Language> Modify(bool IsNewrecord, Language model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[4] = new SqlParameter("@LanguageCultureType", (byte)model.LanguageCultureType);
                    param[5] = new SqlParameter("@UniqueSeoCode", model.UniqueSeoCode);
                    param[6] = new SqlParameter("@UserId", _requestInfo.UserId);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyLanguage", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<Language>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
