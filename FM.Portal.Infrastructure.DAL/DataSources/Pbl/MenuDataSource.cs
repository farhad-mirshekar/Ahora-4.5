using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class MenuDataSource : IMenuDataSource
    {
        private Result<Menu> Modify(bool IsNewRecord, Menu model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[2] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[3] = new SqlParameter("@Name", model.Name);
                    param[4] = new SqlParameter("@ParentID", model.ParentID);
                    param[5] = new SqlParameter("@Node", model.Node);
                    param[6] = new SqlParameter("@Url", model.Url);
                    param[7] = new SqlParameter("@IconText", model.IconText);
                    param[8] = new SqlParameter("@Priority", model.Priority);
                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, param, "pbl.spModifyMenu");
                    return Get(model.ID);
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
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@ParentNode", SqlDbType.NVarChar);
                param[1].SqlValue = DBNull.Value;

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetMenu", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Deleted = SQLHelper.CheckBoolNull(dr["Deleted"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.IconText = SQLHelper.CheckStringNull(dr["IconText"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
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

        public Result<Menu> Get(string ParentNode)
        {
            try
            {
                var obj = new Menu();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                param[0].SqlValue = DBNull.Value;
                param[1] = new SqlParameter("@ParentNode", ParentNode);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetMenu", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Deleted = SQLHelper.CheckBoolNull(dr["Deleted"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.IconText = SQLHelper.CheckStringNull(dr["IconText"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
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
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
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
