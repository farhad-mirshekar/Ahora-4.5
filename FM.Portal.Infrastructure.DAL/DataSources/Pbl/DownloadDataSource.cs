using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class DownloadDataSource : IDownloadDataSource
    {
        public Result<Download> Get(Guid ID)
        {
            try
            {
                var obj = new Download();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetDownload", param))
                    {
                        while (dr.Read())
                        {
                            obj.Comment = SQLHelper.CheckStringNull(dr["Comment"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.PaymentID = SQLHelper.CheckGuidNull(dr["PaymentID"]);
                            obj.ExpireDate = SQLHelper.CheckDateTimeNull(dr["ExpireDate"]);
                            obj.Data = (byte[])dr["Data"];
                        }
                    }

                }
                return Result<Download>.Successful(data: obj);
            }
            catch
            {
                return Result<Download>.Failure();
            }
        }

        public Result<Download> Insert(Download model)
         => Modify(true, model);

        public DataTable List(Guid PaymentID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@PaymentID", PaymentID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetDownloads", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Download> Update(Download model)
        => Modify(false, model);
        private Result<Download> Modify(bool IsNewRecord, Download model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Comment", model.Comment);
                    param[2] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[3] = new SqlParameter("@Data", model.Data);
                    param[4] = new SqlParameter("@UserID", model.UserID);
                    param[5] = new SqlParameter("@PaymentID", model.PaymentID);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyDownload", param);
                    return Get(model.ID);
                }
            }
            catch (Exception e) { return Result<Download>.Failure(); }

        }
    }
}
