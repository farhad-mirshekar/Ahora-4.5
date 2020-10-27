using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class BankDataSource : IBankDataSource
    {
        public Result<Core.Model.Bank> GetActiveBank()
        {
            try
            {
                var obj = new Core.Model.Bank();
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetActiveBank", null))
                    {
                        while (dr.Read())
                        {
                            obj.BankName = (BankName)SQLHelper.CheckByteNull(dr["BankName"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Default = SQLHelper.CheckBoolNull(dr["Default"]);
                            obj.MerchantID = SQLHelper.CheckStringNull(dr["MerchantID"]);
                            obj.MerchantKey = SQLHelper.CheckStringNull(dr["MerchantKey"]);
                            obj.Password = SQLHelper.CheckStringNull(dr["Password"]);
                            obj.RedirectUrl = SQLHelper.CheckStringNull(dr["RedirectUrl"]);
                            obj.TerminalID = SQLHelper.CheckStringNull(dr["TerminalID"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.UserName = SQLHelper.CheckStringNull(dr["UserName"]);

                        }
                    }

                }
                return Result<Core.Model.Bank>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Core.Model.Bank>.Failure(); }
        }
    }
}
