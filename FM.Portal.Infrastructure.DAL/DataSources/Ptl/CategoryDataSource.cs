using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using FM.Portal.DataSource.Ptl;
using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL.Ptl
{
    public class CategoryDataSource : ICategoryDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public CategoryDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsCategory", null);
            }
            catch (Exception e) { throw; }

        }
        private Result<Category> Modify(bool IsNewrecord, Category model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@ParentID", model.ParentID);
                    param[2] = new SqlParameter("@Title", model.Title);
                    param[3] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[4] = new SqlParameter("@Node", model.Node);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyCategory", param);
                    if (result == 0)
                        return Result<Category>.Failure();

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }

        }
        public Result<Category> Insert(Category model)
        => Modify(true, model);
        public Result<Category> Update(Category model)
        => Modify(false, model);
        public Result<Category> Get(Guid ID)
        {
            try
            {
                var obj = new Category();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetCategory", param))
                    {
                        while (dr.Read())
                        {
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.RemoverDate = SQLHelper.CheckDateTimeNull(dr["RemoverDate"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                        }
                    }

                }
                if (obj.ID != Guid.Empty)
                    return Result<Category>.Successful(data: obj);

                return Result<Category>.Successful(data: null);
            }
            catch (Exception e) { return Result<Category>.Failure(); }

        }
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@RemoverID", _requestInfo.UserId);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
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
