
using System;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;

namespace FM.Portal.Infrastructure.DAL
{
    public class PositionDataSource : IPositionDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public PositionDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result<Position> Get(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Result<Position> Insert(Position model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List(PositionListVM listVM)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@DepartmentID",SqlDbType.UniqueIdentifier);
                param[0].Value = (object)listVM.DepartmentID ?? DBNull.Value;

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetsPosition", param);
            }
            catch(Exception e) { throw; }
        }

        public Result<PositionDefaultVM> PositionDefault(Guid userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@UserID", userID);
                PositionDefaultVM obj = new PositionDefaultVM();
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spPositionDefault", param))
                    {
                        while (dr.Read())
                        {
                            obj.UserID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserName = SQLHelper.CheckStringNull(dr["UserName"]);
                            obj.PositionID = SQLHelper.CheckGuidNull(dr["PositionID"]);

                        }
                    }

                }
                return Result<PositionDefaultVM>.Successful(data: obj);
            }
            catch (Exception e) { return Result<PositionDefaultVM>.Failure(); }
            
        }

        public Result<Position> Update(Position model)
        {
            return Modify(false, model);
        }

        private Result<Position> Modify(bool isNewRecord , Position model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@IsNewRecord", isNewRecord);

                    param[1] = new SqlParameter("@ID", model.ID);
                    param[2] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
                    param[3] = new SqlParameter("@Type",(byte) model.Type);
                    param[4] = new SqlParameter("@ParentID", model.ParentID);
                    param[5] = new SqlParameter("@DepartmentID", _requestInfo.DepartmentId);
                    param[6] = new SqlParameter("@UserID", model.UserID);
                    param[7] = new SqlParameter("@RoleIDs", model.Json);
                    SQLHelper.ExecuteNonQuery(con, System.Data.CommandType.StoredProcedure, param, "org.spModifyPosition");
                }

                return Get(model.ID);
            }
            catch (Exception e) { return Result<Position>.Failure(); }
        }
    }
}
