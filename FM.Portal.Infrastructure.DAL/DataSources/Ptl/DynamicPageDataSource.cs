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
    public class DynamicPageDataSource : IDynamicPageDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public DynamicPageDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteDynamicPage", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<DynamicPage> Get(Guid ID)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", ID);
            param[1] = new SqlParameter("@TrackingCode",SqlDbType.NVarChar);
            param[1].Value = DBNull.Value;
            return _Get(param);
        }

        public Result<DynamicPage> Get(string TrackingCode)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            param[0].Value = DBNull.Value;

            param[1] = new SqlParameter("@TrackingCode", TrackingCode);
            return _Get(param);
        }

        public Result<DynamicPage> Insert(DynamicPage model)
        => Modify(true, model);

        public DataTable List(DynamicPageListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("PageID", SqlDbType.UniqueIdentifier);
                param[0].Value = listVM.PageID ?? null;
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsDynamicPage", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<DynamicPage> Update(DynamicPage model)
       => Modify(false, model);
        private Result<DynamicPage> Modify(bool IsNewRecord, DynamicPage model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[10];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Body", model.Body);
                    param[2] = new SqlParameter("@Description", model.Description);
                    param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                    param[4] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[6] = new SqlParameter("@IsShow", (byte)model.IsShow);
                    param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                    param[8] = new SqlParameter("@PageID", model.PageID);
                    param[9] = new SqlParameter("@Name", model.Name);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyDynamicPage", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<DynamicPage>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
        private Result<DynamicPage> _Get(SqlParameter[] param)
        {
            try
            {
                var obj = new DynamicPage();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetDynamicPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.IsShow = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["IsShow"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.PageID = SQLHelper.CheckGuidNull(dr["PageID"]);
                            obj.PageName = SQLHelper.CheckStringNull(dr["PageName"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (Core.Model.PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.TrackingCodeParent = SQLHelper.CheckStringNull(dr["TrackingCodeParent"]);
                        }
                    }

                }
                return Result<DynamicPage>.Successful(data: obj);
            }
            catch
            {
                return Result<DynamicPage>.Failure();
            }
        }
    }
}
