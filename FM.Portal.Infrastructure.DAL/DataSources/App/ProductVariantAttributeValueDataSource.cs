using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductVariantAttributeValueDataSource : IProductVariantAttributeValueDataSource
    {
        private Result<ProductVariantAttributeValue> Modify(bool IsNewRecord , ProductVariantAttributeValue model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@IsPreSelected", model.IsPreSelected);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@ProductVariantAttributeID", model.ProductVariantAttributeID);
                    param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[5] = new SqlParameter("@Price", model.Price);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProductVariantAttributeValue", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<ProductVariantAttributeValue> Get(Guid ID)
        {
            try
            {
                var obj = new ProductVariantAttributeValue();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProductVariantAttributeValue", param))
                    {
                        while (dr.Read())
                        {
                            obj.IsPreSelected = SQLHelper.CheckBoolNull(dr["IsPreSelected"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.ProductVariantAttributeID = SQLHelper.CheckGuidNull(dr["ProductVariantAttributeID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                        }
                    }

                }
                return Result<ProductVariantAttributeValue>.Successful(data: obj);
            }
            catch (Exception e) { return Result<ProductVariantAttributeValue>.Failure(); }
        }

        public Result<ProductVariantAttributeValue> Insert(ProductVariantAttributeValue model)
        {
            return Modify(true, model);
        }

        public DataTable List(Guid ProductVariantAttributeID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ProductVariantAttributeID", ProductVariantAttributeID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductVariantAttributeValue", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<ProductVariantAttributeValue> Update(ProductVariantAttributeValue model)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteProductVariantAttributeValue", param);
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
