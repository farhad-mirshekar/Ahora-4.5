using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductAttributeDataSource : IProductAttributeDataSource
    {
        private Result<ProductAttribute> Modify(bool isNewRecord , ProductAttribute model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Description", model.Description);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@IsNewRecord", isNewRecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProductAttribute", param);

                    return Get(model.ID);
                }
            }
            catch { return Result<ProductAttribute>.Failure(); }
        }
        public Result<ProductAttribute> Get(Guid ID)
        {
            try
            {
                ProductAttribute obj = new ProductAttribute();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProductAttribute", param))
                    {
                        while (dr.Read())
                        {
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                        }
                    }

                }
                return Result<ProductAttribute>.Successful(data: obj);
            }
            catch
            {
                return Result<ProductAttribute>.Failure();
            }
        }

        public Result<ProductAttribute> Insert(ProductAttribute model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductAttribute", null);
        }

        public Result<ProductAttribute> Update(ProductAttribute model)
        {
            return Modify(false, model);
        }
    }
}
