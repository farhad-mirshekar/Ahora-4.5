﻿using System;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using FM.Portal.Core.Owin;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;
using System.Web;

namespace FM.Portal.Infrastructure.DAL
{
    public class UserAddressDataSource : IUserAddressDataSource
    {
        private Result<UserAddress> Modify(bool IsNewRecord, UserAddress model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    param[1] = new SqlParameter("@ID", model.ID);
                    param[2] = new SqlParameter("@UserID", model.UserID);
                    param[3] = new SqlParameter("@Address", model.Address);
                    param[4] = new SqlParameter("@PostalCode", model.PostalCode);
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spModifyUserAddress", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<UserAddress>.Failure(message: "خطا در درج / ویرایش");
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Result<UserAddress> Insert(UserAddress model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<UserAddress> Update(UserAddress model)
        {
            return Modify(false, model);
        }

        public Result<UserAddress> Get(Guid ID)
        {
            try
            {
                UserAddress obj = new UserAddress();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetUserAddress", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.Address = SQLHelper.CheckStringNull(dr["Address"]);
                            obj.PostalCode = SQLHelper.CheckStringNull(dr["PostalCode"]);
                        }
                    }

                }
                return Result<UserAddress>.Successful(data: obj);
            }
            catch
            {
                return Result<UserAddress>.Failure();
            }
        }

        public DataTable List(UserAddressListVM listVM)
        {
            var param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", listVM.UserID);
            param[1] = new SqlParameter("@PageSize", listVM.PageSize);
            param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetsUserAddress", param);
        }

        public Result Delete(Guid ID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", ID);
            int result = 0;
            using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spDeleteUserAddress", param);
            }
            return Result.Successful();
        }
    }
}
