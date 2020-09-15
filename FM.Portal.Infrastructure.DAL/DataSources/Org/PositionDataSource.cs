
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
            try
            {
                var obj = new Position();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetPosition", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.ApplicationID = SQLHelper.CheckGuidNull(dr["ApplicationID"]);
                            //obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Default = SQLHelper.CheckBoolNull(dr["Default"]);
                            obj.DepartmentID = SQLHelper.CheckGuidNull(dr["DepartmentID"]);
                            obj.DepartmentName = SQLHelper.CheckStringNull(dr["DepartmentName"]);
                            obj.Enabled = SQLHelper.CheckBoolNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Type = (PositionType)SQLHelper.CheckByteNull(dr["Type"]);
                            obj.UserType = (UserType)SQLHelper.CheckByteNull(dr["UserType"]);
                        }
                    }

                }
                return Result<Position>.Successful(data: obj);

            }
            catch(Exception e) { throw; }
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
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@DepartmentID",SqlDbType.UniqueIdentifier);
                param[0].Value = listVM.DepartmentID != null ? listVM.DepartmentID :(object) DBNull.Value;

                param[1] = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
                param[1].Value = (object)listVM.UserID ?? DBNull.Value;

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
                            obj.PositionType = (PositionType)SQLHelper.CheckByteNull(dr["Type"]);

                        }
                    }

                }
                return Result<PositionDefaultVM>.Successful(data: obj);
            }
            catch (Exception e) { return Result<PositionDefaultVM>.Failure(); }
            
        }

        public Result SetDefault(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spSetDefaultPosition", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
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
                    param[5] = new SqlParameter("@DepartmentID", model.DepartmentID);
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
