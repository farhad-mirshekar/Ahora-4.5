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
        public Result<CategoryMapDiscount> Get(Guid? CategoryID, Guid? DiscountID)
        {
            try
            {
                var obj = new CategoryMapDiscount();
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@CategoryID", CategoryID);
                param[1] = new SqlParameter("@DiscountID", DiscountID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetCategoryMapDiscount", param))
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

        public Result<CategoryMapDiscount> Insert(CategoryMapDiscount model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[4];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@CategoryID", model.CategoryID);
                    param[2] = new SqlParameter("@DiscountID", model.DiscountID);
                    param[3] = new SqlParameter("@IsNewRecord", true);
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyCategoryMapDiscount", param);
                    if (result > 0)
                        return Get(model.ID, null);
                    return Result<CategoryMapDiscount>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
