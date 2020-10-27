using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class RelatedProductDataSource : IRelatedProductDataSource
    {
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteRelatedProduct", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<RelatedProduct> Get(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                var obj = new RelatedProduct();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetrelatedProduct", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.ProductID1 = SQLHelper.CheckGuidNull(dr["ProductID1"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ProductID2 = SQLHelper.CheckGuidNull(dr["ProductID2"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                        }
                    }

                }
                return Result<RelatedProduct>.Successful(data: obj);
            }
            catch
            {
                return Result<RelatedProduct>.Failure();
            }
        }

        public Result<RelatedProduct> Insert(RelatedProduct model)
        => Modify(true, model);

        public DataTable List(RelatedProductListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("ProductID1", SqlDbType.UniqueIdentifier);
                param[0].Value = listVM.ProductID1 != null ? listVM.ProductID1.Value : (object)DBNull.Value;

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsRelatedProduct", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<RelatedProduct> Update(RelatedProduct model)
        => Modify(false, model);

        #region Private
        private Result<RelatedProduct> Modify(bool IsNewRecord , RelatedProduct model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@ProductID1", model.ProductID1);
                    param[2] = new SqlParameter("@ProductID2", model.ProductID2);
                    param[3] = new SqlParameter("@Priority", model.Priority);
                    param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyRelatedProduct", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<RelatedProduct>.Failure(message:"خطا در درج / ویرایش");
                }
                    

            }
            catch(Exception e) { return Result<RelatedProduct>.Failure(); }
        }
        #endregion
    }
}
