using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using FM.Portal.DataSource.Ptl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL.Ptl
{
   public class CategoryDataSource : ICategoryDataSource
    {
        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsCategory", null);
            }
            catch (Exception e) { throw; }

        }
        private Result<Category> Modify(bool isNewrecord, Category model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@IncludeInTopMenu", model.IncludeInTopMenu);
                    param[2] = new SqlParameter("@ParentID", model.ParentID);
                    param[3] = new SqlParameter("@IncludeInLeftMenu", model.IncludeInLeftMenu);
                    param[4] = new SqlParameter("@Title", model.Title);
                    param[5] = new SqlParameter("@isNewRecord", isNewrecord);
                    param[6] = new SqlParameter("@Node", model.Node);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyCategory", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }

        }
        public Result<Category> Insert(Category model)
        {
            return Modify(true, model);
        }
        public Result<Category> Update(Category model)
        {
            return Modify(false, model);
        }
        public Result<Category> Get(Guid ID)
        {
            try
            {
                var obj = new Category();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetCategory", param))
                    {
                        while (dr.Read())
                        {
                            obj.IncludeInTopMenu = SQLHelper.CheckBoolNull(dr["IncludeInTopMenu"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.IncludeInLeftMenu = SQLHelper.CheckBoolNull(dr["IncludeInLeftMenu"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                        }
                    }

                }
                return Result<Category>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Category>.Failure(); }

        }
        public DataTable GetCountCategory()
        {

            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetCountCategory", null);
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteCategory", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
