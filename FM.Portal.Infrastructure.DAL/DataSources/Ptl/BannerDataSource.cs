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
    public class BannerDataSource : IBannerDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public BannerDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteBanner", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<Banner> Get(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                var obj = new Banner();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetBanner", param))
                    {
                        while (dr.Read())
                        {
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (Core.Model.PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.BannerType = (Core.Model.BannerType)SQLHelper.CheckByteNull(dr["BannerType"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                        }
                    }

                }
                return Result<Banner>.Successful(data: obj);
            }
            catch
            {
                return Result<Banner>.Failure();
            }
        }

        public Result<Banner> Insert(Banner model)
        => Modify(true, model);

        public DataTable List(BannerListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@BannerType", SqlDbType.TinyInt);
                param[0].Value = listVM.BannerType != BannerType.نامشخص ? listVM.BannerType : (object)DBNull.Value;

                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsBanner", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Banner> Update(Banner model)
        => Modify(false, model);
        private Result<Banner> Modify(bool IsNewRecord, Banner model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Description", model.Description);
                    param[2] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[3] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[4] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[5] = new SqlParameter("@BannerType", (byte)model.BannerType);
                    param[6] = new SqlParameter("@Name", model.Name);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyBanner", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<Banner>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
