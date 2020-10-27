using System;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Data.SqlClient;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class CategoryMapDiscountDataSource : ICategoryMapDiscountDataSource
    {
        public Result<CategoryMapDiscount> Create(CategoryMapDiscount model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    model.ID = Guid.NewGuid();
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IncludeInTopMenu", model.CategoryID);
                    param[2] = new SqlParameter("@ParentID", model.DiscountID);
                    param[3] = new SqlParameter("@isNewRecord", true);
                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyCategoryMapDiscount", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }

        private Result<CategoryMapDiscount> Get(Guid ID)
        {
            try
            {
                var obj = new CategoryMapDiscount();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetCategoryMapDiscount", param))
                    {
                        while (dr.Read())
                        {
                            obj.CategoryID = SQLHelper.CheckGuidNull(dr["CategoryID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.DiscountID = SQLHelper.CheckGuidNull(dr["DiscountID"]);
                            obj.Active = SQLHelper.CheckBoolNull(dr["Active"]);
                        }
                    }

                }
                return Result<CategoryMapDiscount>.Successful(data: obj);
            }

            catch (Exception e) { return Result<CategoryMapDiscount>.Failure(); }
        }

    }
}
