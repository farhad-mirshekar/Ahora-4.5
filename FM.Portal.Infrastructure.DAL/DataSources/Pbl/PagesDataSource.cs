using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class PagesDataSource : IPagesDataSource
    {
        private Result<Pages> Modify(bool IsNewRecord, Pages model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[10];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@ActionName", model.ActionName);
                    param[2] = new SqlParameter("@ControllerName", model.ControllerName);
                    param[3] = new SqlParameter("@Description", model.Description);
                    param[4] = new SqlParameter("@Enabled", (bool)model.Enabled);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[6] = new SqlParameter("@Title", model.Title);
                    param[7] = new SqlParameter("@ParentID", model.ParentID);
                    param[8] = new SqlParameter("@Node", model.Node);
                    param[9] = new SqlParameter("@RouteUrl", model.RouteUrl);
                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, param, "pbl.spModifyPages");
                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<Pages> Create(Pages model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<Pages> Get(Guid ID)
        {
            try
            {
                var obj = new Pages();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.ActionName = SQLHelper.CheckStringNull(dr["ActionName"]);
                            obj.ControllerName = SQLHelper.CheckStringNull(dr["ControllerName"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Deleted = SQLHelper.CheckBoolNull(dr["Deleted"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.Enabled = SQLHelper.CheckBoolNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.RouteUrl = SQLHelper.CheckStringNull(dr["RouteUrl"]);
                        }
                    }

                }
                return Result<Pages>.Successful(data: obj);
            }
            catch
            {
                return Result<Pages>.Failure();
            }
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsPage", null);
            }
            catch (Exception e) { throw; }
        }

        public Result<Pages> Update(Pages model)
        {
            return Modify(false, model);
        }

        public Result Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeletePages", param);
                    if(i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
                    
            }
            catch(Exception e) { throw; }
        }
    }
}
