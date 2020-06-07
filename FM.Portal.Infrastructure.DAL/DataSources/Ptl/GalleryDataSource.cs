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
    public class GalleryDataSource : IGalleryDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public GalleryDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteGallery", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<Gallery> Get(Guid ID)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", ID);
            param[1] = new SqlParameter("@TrackingCode", SqlDbType.NVarChar);
            param[1].Value = DBNull.Value;
            return _Get(param);
        }

        public Result<Gallery> Get(string TrackingCode)
        {
            var param = new SqlParameter[2];
            param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            param[0].Value = DBNull.Value;

            param[1] = new SqlParameter("@TrackingCode", TrackingCode);
            return _Get(param);
        }

        public Result<Gallery> Insert(Gallery model)
        => Modify(true, model);

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsGallery", null);
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(int Count)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                Count = Count == 0 ? 4 : Count;
                param[0] = new SqlParameter("@Count", Count);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsGalleryByCount", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Gallery> Update(Gallery model)
        => Modify(false, model);
        private Result<Gallery> Modify(bool IsNewRecord, Gallery model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Description", model.Description);
                    param[2] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[3] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[4] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[5] = new SqlParameter("@VisitedCount", model.VisitedCount);
                    param[6] = new SqlParameter("@Name", model.Name);
                    param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                    param[8] = new SqlParameter("@UrlDesc", model.UrlDesc);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyGallery", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<Gallery>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
        private Result<Gallery> _Get(SqlParameter[] param)
        {
            try
            {
                var obj = new Gallery();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetGallery", param))
                    {
                        while (dr.Read())
                        {
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
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                        }
                    }

                }
                return Result<Gallery>.Successful(data: obj);
            }
            catch
            {
                return Result<Gallery>.Failure();
            }
        }
    }
}
