using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class RoleDataSource : IRoleDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public RoleDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result Delete(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Result<Role> Get(Guid id)
        {
            try
            {
                Role obj = new Role();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", id);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetRole", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.ApplicationID = SQLHelper.CheckGuidNull(dr["ApplicationID"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);

                        }
                    }

                }
                return Result<Role>.Successful(data: obj);
            }
            catch
            {
                return Result<Role>.Failure();
            }
        }

        public Result<Role> Insert(Role model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            SqlParameter[] param = new SqlParameter[3];
            var t = _requestInfo.UserName;
            param[0] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
            param[1] = new SqlParameter("@PositionID", null);
            param[2] = new SqlParameter("@UserID", null);

            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetRoles", param);
        }

        public Result<Role> Update(Role model)
        {
            return Modify(false, model);
        }

        private Result<Role> Modify(bool isNewRecord, Role model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@IsNewRecord", isNewRecord);

                    param[1] = new SqlParameter("@ID", model.ID);
                    param[2] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
                    param[3] = new SqlParameter("@Name", model.Name);
                    param[4] = new SqlParameter("@Permissions", model.Json);
                    SQLHelper.ExecuteNonQuery(con, System.Data.CommandType.StoredProcedure, param, "org.spModifyRole");
                }

                return Get(model.ID);
            }
            catch (Exception e) { return Result<Role>.Failure(); }
        }
    }
}
