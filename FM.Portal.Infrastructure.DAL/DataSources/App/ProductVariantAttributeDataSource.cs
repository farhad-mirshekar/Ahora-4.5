using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductVariantAttributeDataSource : IProductVariantAttributeDataSource
    {
        private Result<ProductVariantAttribute> Modify(bool IsNewRecord , ProductVariantAttribute model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@IsPreSelected", model.IsPreSelected);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@ProductVariantAttributeID", model.ProductVariantAttributeID);
                    param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProductVariantAttribute", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<ProductVariantAttribute> Get(Guid ID)
        {
            try
            {
                ProductVariantAttribute obj = new ProductVariantAttribute();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProductVariantAttribute", param))
                    {
                        while (dr.Read())
                        {
                            obj.IsPreSelected = SQLHelper.CheckBoolNull(dr["IsPreSelected"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.ProductVariantAttributeID = SQLHelper.CheckGuidNull(dr["ProductVariantAttributeID"]);
                        }
                    }

                }
                return Result<ProductVariantAttribute>.Successful(data: obj);
            }
            catch (Exception e) { return Result<ProductVariantAttribute>.Failure(); }
        }

        public Result<ProductVariantAttribute> Insert(ProductVariantAttribute model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List(Guid ProductVariantAttributeID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ProductVariantAttributeID", ProductVariantAttributeID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductVariantAttribute", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<ProductVariantAttribute> Update(ProductVariantAttribute model)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteProductVariantAttribute", param);
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
