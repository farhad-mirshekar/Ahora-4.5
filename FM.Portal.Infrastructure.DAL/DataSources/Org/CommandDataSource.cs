
using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using System.Configuration;

namespace FM.Portal.Infrastructure.DAL
{
    public class CommandDataSource : ICommandDataSource
    {
        private readonly IRequestInfo _requestInfo;
        private readonly IAppSetting _appSetting;
        public CommandDataSource(IRequestInfo requestInfo
                                ,IAppSetting appSetting)
        {
            _requestInfo = requestInfo;
            _appSetting = appSetting;
        }
        private Result<Command> Modify(bool isNewRecord, Command model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@IsNewRecord", isNewRecord);

                    param[1] = new SqlParameter("@ID", model.ID);
                    param[2] = new SqlParameter("@ParentID", model.ParentID);
                    param[3] = new SqlParameter("@Node", model.Node);
                    param[4] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
                    param[5] = new SqlParameter("@Name", model.Name);
                    param[6] = new SqlParameter("@FullName", model.FullName);
                    param[7] = new SqlParameter("@Title", model.Title);
                    param[8] = new SqlParameter("@Type", (byte)model.Type);
                    SQLHelper.ExecuteNonQuery(con, System.Data.CommandType.StoredProcedure, param, "org.spModifyCommand");
                }

                return Get(model.ID);
            }
            catch (Exception e) { throw; }
        }
        public Result Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@ApplicationID", _appSetting.ApplicationID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spDeleteCommand", param);
                    if(result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch(Exception e) { throw; }
        }

        public Result<Command> Get(Guid ID)
        {
            try
            {
                Command obj = new Command();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetCommand", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.FullName = SQLHelper.CheckStringNull(dr["FullName"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            //obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.Type = (CommandsType)SQLHelper.CheckByteNull(dr["Type"]);

                        }
                    }

                }
                return Result<Command>.Successful(data: obj);
            }
            catch
            {
                return Result<Command>.Failure();
            }
        }

        public Result<Command> Insert(Command model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetCommands", null);
        }
        public DataTable ListForRole(CommandListVM model)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
            param[1] = new SqlParameter("@RoleID", model.RoleID);

            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetCommandsForRole", param);
        }

        public Result<Command> Update(Command model)
        {
            return Modify(false, model);
        }

        public DataTable ListByNode(string Node)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Node", Node);
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetCommandByNode", param);
        }
        public DataTable GetPermission()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PositionID", _requestInfo.PositionId);
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetCommandsForUser", param);
        }

    }
}
