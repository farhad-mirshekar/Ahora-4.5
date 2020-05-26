using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class ContactDataSource : IContactDataSource
    {
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteContact", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Contact> Get(Guid ID)
        {
            try
            {
                var obj = new Contact();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetContact", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.Email = SQLHelper.CheckStringNull(dr["Email"]);
                            obj.FirstName = SQLHelper.CheckStringNull(dr["FirstName"]);
                            obj.LastName = SQLHelper.CheckStringNull(dr["LastName"]);
                            obj.Phone = SQLHelper.CheckStringNull(dr["Phone"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                        }
                    }

                }
                return Result<Contact>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Contact>.Failure(); }
        }

        public Result<Contact> Insert(Contact model)
        => Modify(true, model);

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsContact", null);
            }
            catch (Exception e) { throw; }
        }
        private Result<Contact> Modify(bool IsNewrecord, Contact model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@isNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@Title", model.Title);
                    param[3] = new SqlParameter("@FirstName", model.FirstName);
                    param[4] = new SqlParameter("@LastName", model.LastName);
                    param[5] = new SqlParameter("@Phone", model.Phone);
                    param[6] = new SqlParameter("@Email", model.Email);
                    param[7] = new SqlParameter("@Description", model.Description);

                  int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyContact", param);
                    if(result > 0)
                        return Get(model.ID);
                    return Result<Contact>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
