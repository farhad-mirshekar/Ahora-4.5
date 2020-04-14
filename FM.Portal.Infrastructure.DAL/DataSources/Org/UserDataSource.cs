﻿using System;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Security;
using System.Data;
using FM.Portal.Core.Result;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class UserDataSource : IUserDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public UserDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        private Result<User> Modify(bool isNewRecord, User model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[14];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@isNewRecord", isNewRecord);
                    param[2] = new SqlParameter("@Enabled", model.Enabled);
                    param[3] = new SqlParameter("@Username", model.Username);
                    param[4] = new SqlParameter("@Password", model.Password);
                    param[5] = new SqlParameter("@FirstName", model.FirstName);
                    param[6] = new SqlParameter("@LastName", model.LastName);
                    param[7] = new SqlParameter("@NationalCode", model.NationalCode);
                    param[8] = new SqlParameter("@Email", model.Email);
                    param[9] = new SqlParameter("@EmailVerified", model.EmailVerified);
                    param[10] = new SqlParameter("@CellPhone", model.CellPhone);
                    param[11] = new SqlParameter("@CellPhoneVerified", model.CellPhoneVerified);
                    param[12] = new SqlParameter("@PasswordExpireDate", model.PasswordExpireDate);
                    param[13] = new SqlParameter("@Type", (byte)model.Type);

                    SQLHelper.ExecuteNonQuery(con, System.Data.CommandType.StoredProcedure, param, "org.spModifyUser");

                    return this.Get(model.ID, null, null, null);
                }
            }
            catch
            {
                return Result<User>.Failure();
            }
        }
        public Result<User> Get(Guid? ID, string Username, string Password , string NationalCode)
        {
            try
            {
                User obj = new User();
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                if (ID.HasValue)
                    param[0].Value = ID;
                else
                    param[0].Value = DBNull.Value;

                param[1] = new SqlParameter("@Username", SqlDbType.VarChar);
                if (Username == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = Username;

                param[2] = new SqlParameter("@Password", SqlDbType.VarChar);
                if (Password == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Password;

                param[3] = new SqlParameter("@NationalCode", SqlDbType.VarChar);
                if (NationalCode == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = NationalCode;

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetUser", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.LastName = SQLHelper.CheckStringNull(dr["LastName"]);
                            obj.FirstName = SQLHelper.CheckStringNull(dr["FirstName"]);
                            obj.Username = SQLHelper.CheckStringNull(dr["Username"]);
                            obj.NationalCode = SQLHelper.CheckStringNull(dr["NationalCode"]);
                            obj.CellPhone = SQLHelper.CheckStringNull(dr["CellPhone"]);
                            obj.Enabled = SQLHelper.CheckBoolNull(dr["Enabled"]);
                            obj.Type = (UserType)SQLHelper.CheckByteNull(dr["UserType"]);

                        }
                    }

                }
                return Result<User>.Successful(data: obj);
            }
            catch
            {
                return Result<User>.Failure();
            }
        }

        public Result<User> Insert(User model)
        {
            return Modify(true, model);
        }

        public Result<User> Update(User model)
        {
            return Modify(false, model);
        }

        public Result SetPassword(SetPasswordVM model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@ID",_requestInfo.UserId);
                    param[1] = new SqlParameter("@Password", model.NewPassword);
                    param[2] = new SqlParameter("@PasswordExpireDate", DateTime.Now);
                   int i= SQLHelper.CheckIntNull(SQLHelper.ExecuteScalar(con, CommandType.StoredProcedure, "org.spSetUserPassword", param));
                    if (i > 0)
                        return Result.Successful(message:"تغییر رمز عبور با موفقیت انجام گرفت");
                    else
                    return Result.Failure(message:"خطایی رخ داده است");
                }
            }
            catch(Exception e) { throw; }
        }

    }
}
