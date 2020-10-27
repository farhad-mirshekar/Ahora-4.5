using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class EmailLogsDataSource : IEmailLogsDataSource
    {
        public Result<EmailLogs> Get(Guid ID)
        {
            try
            {
                var obj = new EmailLogs();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetEmailLogs", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.EmailStatusType = (EmailStatusType)SQLHelper.CheckByteNull(dr["EmailStatusType"]);
                            obj.From = SQLHelper.CheckStringNull(dr["From"]);
                            obj.IP = SQLHelper.CheckStringNull(dr["IP"]);
                            obj.Message = SQLHelper.CheckStringNull(dr["Message"]);
                            obj.To = SQLHelper.CheckStringNull(dr["To"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                        }
                    }
                }
                return Result<EmailLogs>.Successful(data: obj);
            }
            catch(Exception e) { throw; }
        }

        public Result<EmailLogs> Insert(EmailLogs model)
        => Modify(true, model);

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsEmailLogs", null);
            }
            catch (Exception e) { throw; }
        }

        private Result<EmailLogs> Modify(bool IsNewRecord , EmailLogs model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[2] = new SqlParameter("@EmailStatusType", (byte)model.EmailStatusType);
                    param[3] = new SqlParameter("@From", model.From);
                    param[4] = new SqlParameter("@IP", model.IP);
                    param[5] = new SqlParameter("@Message", model.Message);
                    param[6] = new SqlParameter("@To", model.To);
                    param[7] = new SqlParameter("@UserID", model.UserID);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyEmailLogs", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<EmailLogs>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
