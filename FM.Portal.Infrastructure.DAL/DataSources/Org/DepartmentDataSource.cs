using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class DepartmentDataSource : IDepartmentDataSource
    {
        public Result<int> Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "org.spDeleteDepartment", param);
                    if (result > 0)
                        return Result<int>.Successful(data:result);
                    else
                        return Result<int>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Department> Get(Guid ID)
        {
            try
            {
                var obj = new Department();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "org.spGetDepartment", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Address = SQLHelper.CheckStringNull(dr["Address"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.CodePhone = SQLHelper.CheckStringNull(dr["CodePhone"]);
                            obj.Phone =SQLHelper.CheckStringNull(dr["Phone"]);
                            obj.PostalCode = SQLHelper.CheckStringNull(dr["PostalCode"]);
                            obj.RemoverDate = SQLHelper.CheckDateTimeNull(dr["RemoverDate"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);

                        }
                    }

                }
                return Result<Department>.Successful(data: obj);
            }
            catch(Exception e)
            {
                return Result<Department>.Failure();
            }
        }

        public Result<Department> Insert(Department model)
        => Modify(true, model);

        public DataTable List()
        {
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetsDepartment", null);
        }

        public DataTable ListByNode(string Node)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Node", Node);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "org.spGetsDepartmentByNode", param);
            }
            catch { throw; }
        }

        public Result<Department> Update(Department model)
        => Modify(false, model);
        private Result<Department> Modify(bool IsNewRecord, Department model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    param[1] = new SqlParameter("@ID", model.ID);
                    param[2] = new SqlParameter("@ParentID", model.ParentID);
                    param[3] = new SqlParameter("@Node", model.Node);
                    param[4] = new SqlParameter("@Name", model.Name);
                    param[5] = new SqlParameter("@Address", model.Address);
                    param[6] = new SqlParameter("@CodePhone", model.CodePhone);
                    param[7] = new SqlParameter("@Phone", model.Phone);
                    param[8] = new SqlParameter("@PostalCode", model.PostalCode);

                    SQLHelper.ExecuteNonQuery(con, System.Data.CommandType.StoredProcedure, param, "org.spModifyDepartment");
                }

                return Get(model.ID);
            }
            catch (Exception e) { throw; }
        }
    }
}
