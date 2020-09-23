using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class StaticPageDataSource : IStaticPageDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public StaticPageDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteStaticPage", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<StaticPage> Get(Guid ID)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", ID);
            param[1] = new SqlParameter("@TrackingCode", SqlDbType.NVarChar);
            param[1].Value = DBNull.Value;
            return _Get(param);
        }

        public Result<StaticPage> Get(string TrackingCode)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            param[0].Value = DBNull.Value;

            param[1] = new SqlParameter("@TrackingCode", TrackingCode);
            return _Get(param);
        }
        public DataTable List(StaticPageListVM listVM)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@Name", listVM.Name);
                param[1] = new SqlParameter("@TrackingCode", listVM.TrackingCode);
                param[2] = new SqlParameter("@PageSize", listVM.PageSize);
                param[3] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsStaticPage", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<StaticPage> Update(StaticPage model)
       => Modify(false, model);

        private Result<StaticPage> Modify(bool IsNewRecord, StaticPage model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[11];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Body", model.Body);
                    param[2] = new SqlParameter("@Description", model.Description);
                    param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                    param[4] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[6] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                    param[8] = new SqlParameter("@AttachmentID", model.AttachmentID);
                    param[9] = new SqlParameter("@BannerShow", model.BannerShow);
                    param[10] = new SqlParameter("@Name", model.Name);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyStaticPage", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<StaticPage>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }

        private Result<StaticPage> _Get(SqlParameter[] param)
        {
            try
            {
                var obj = new StaticPage();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetStaticPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (Core.Model.PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.AttachmentID = SQLHelper.CheckGuidNull(dr["AttachmentID"]);
                            obj.BannerShow = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["BannerShow"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                        }
                    }

                }
                return Result<StaticPage>.Successful(data: obj);
            }
            catch
            {
                return Result<StaticPage>.Failure();
            }
        }
    }
}
