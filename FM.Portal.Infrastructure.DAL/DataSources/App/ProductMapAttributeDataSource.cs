using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductMapAttributeDataSource : IProductMapAttributeDataSource
    {
        private Result<ProductMapAttribute> Modify(bool IsNewRecord , ProductMapAttribute model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@AttributeControlType", (byte) model.AttributeControlType);
                    param[2] = new SqlParameter("@IsRequired", model.IsRequired);
                    param[3] = new SqlParameter("@ProductAttributeID", model.ProductAttributeID);
                    param[4] = new SqlParameter("@ProductID", model.ProductID);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[6] = new SqlParameter("@TextPrompt", model.TextPrompt);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProductMapAttribute", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<ProductMapAttribute> Get(Guid ID)
        {
            try
            {
                ProductMapAttribute obj = new ProductMapAttribute();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProductMapAttribute", param))
                    {
                        while (dr.Read())
                        {
                            obj.AttributeControlType = (AttributeControlType)SQLHelper.CheckByteNull(dr["AttributeControlType"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.IsRequired = SQLHelper.CheckBoolNull(dr["IsRequired"]);
                            obj.ProductAttributeID = SQLHelper.CheckGuidNull(dr["ProductAttributeID"]);
                            obj.ProductID = SQLHelper.CheckGuidNull(dr["ProductID"]);
                            obj.TextPrompt = SQLHelper.CheckStringNull(dr["TextPrompt"]);
                        }
                    }

                }
                return Result<ProductMapAttribute>.Successful(data: obj);
            }
            catch (Exception e) { return Result<ProductMapAttribute>.Failure(); }
        }

        public Result<ProductMapAttribute> Insert(ProductMapAttribute model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List(Guid ProductID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ProductID", ProductID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductMapAttribute", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<ProductMapAttribute> Update(ProductMapAttribute model)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteProductMapAttribute", param);
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
