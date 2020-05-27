using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource.Ptl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL.Ptl
{
    public class PagesDataSource : IPagesDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public PagesDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        private Result<Pages> Modify(bool IsNewRecord, Pages model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Name", model.Name);
                    param[2] = new SqlParameter("@PageType", (byte)model.PageType);
                    param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                    param[4] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[6] = new SqlParameter("@Enabled", model.Enabled);
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyPages",param);
                    if(result > 0)
                        return Get(model.ID);
                    return Result<Pages>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<Pages> Insert(Pages model)
          => Modify(true, model);

        public Result<Pages> Get(Guid ID)
        {
            try
            {
                var obj = new Pages();
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[0].Value = DBNull.Value;

                param[1] = new SqlParameter("@TrackingCode", SqlDbType.NVarChar);
                param[1].Value = DBNull.Value;

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.PageType = (Core.Model.PageType)SQLHelper.CheckByteNull(dr["PageType"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                        }
                    }

                }
                return Result<Pages>.Successful(data: obj);
            }
            catch
            {
                return Result<Pages>.Failure();
            }
        }

        public DataTable List(PagesListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@PageType", SqlDbType.TinyInt);
                param[0].Value = listVM.PageType != Core.Model.PageType.نامشخص ? listVM.PageType : (object)DBNull.Value;
                
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsPage", param);
            }
            catch (Exception e) { throw; }
        }
        public Result<Pages> Update(Pages model)
            => Modify(false, model);

        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeletePages", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<Pages> Get(string TrackingCode)
        {
            try
            {
                var obj = new Pages();
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                param[0].Value = DBNull.Value;

                param[1] = new SqlParameter("@TrackingCode", TrackingCode);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.PageType = (Core.Model.PageType)SQLHelper.CheckByteNull(dr["PageType"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                        }
                    }

                }
                return Result<Pages>.Successful(data: obj);
            }
            catch
            {
                return Result<Pages>.Failure();
            }
        }
    }
}
