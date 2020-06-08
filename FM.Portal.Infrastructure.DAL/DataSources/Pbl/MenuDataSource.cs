using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class MenuDataSource : IMenuDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public MenuDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        private Result<Menu> Modify(bool IsNewRecord, Menu model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[11];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[2] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[3] = new SqlParameter("@Name", model.Name);
                    param[4] = new SqlParameter("@ParentID", model.ParentID);
                    param[5] = new SqlParameter("@Node", model.Node);
                    param[6] = new SqlParameter("@Url", model.Url);
                    param[7] = new SqlParameter("@IconText", model.IconText);
                    param[8] = new SqlParameter("@Priority", model.Priority);
                    param[9] = new SqlParameter("@Parameters", model.Parameters);
                    param[10] = new SqlParameter("@ForeignLink", (byte)model.ForeignLink);

                    var result =  SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyMenu", param);
                    if(result > 0)
                        return Get(model.ID);
                    return Result<Menu>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<Menu> Create(Menu model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<Menu> Get(Guid ID)
        {
            try
            {
                var obj = new Menu();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetMenu", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.IconText = SQLHelper.CheckStringNull(dr["IconText"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                            obj.Parameters = SQLHelper.CheckStringNull(dr["Parameters"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.ForeignLink = (EnableMenuType)SQLHelper.CheckByteNull(dr["ForeignLink"]);
                        }
                    }

                }
                return Result<Menu>.Successful(data: obj);
            }
            catch
            {
                return Result<Menu>.Failure();
            }
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsMenu", null);
            }
            catch (Exception e) { throw; }
        }

        public Result<Menu> Update(Menu model)
        {
            return Modify(false, model);
        }

        public DataTable GetChildren(string ParentNode)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ParentNode", ParentNode);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetChildMenu", param);
            }
            catch (Exception e) { throw; }
        }

        public Result Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@RemoverID", _requestInfo.UserId);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteMenu", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }
    }
}
