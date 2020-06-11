using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class ProductTypeDataSource : IProductTypeDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public ProductTypeDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteProductType", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<ProductType> Get(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                var obj = new ProductType();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetProductType", param))
                    {
                        while (dr.Read())
                        {
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                        }
                    }

                }
                return Result<ProductType>.Successful(data: obj);
            }
            catch
            {
                return Result<ProductType>.Failure();
            }
        }

        public Result<ProductType> Insert(ProductType model)
        => Modify(true, model);

        public DataTable List(ProductTypeListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@Enabled", SqlDbType.TinyInt);
                param[0].Value = listVM.Enabled != EnableMenuType.نامشخص ? listVM.Enabled : (object)DBNull.Value;

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsProductType", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<ProductType> Update(ProductType model)
        => Modify(false, model);

        private Result<ProductType> Modify(bool IsNewRecord, ProductType model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Description", model.Description);
                    param[2] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[3] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[4] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[5] = new SqlParameter("@Price", model.Price);
                    param[6] = new SqlParameter("@Name", model.Name);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyProductType", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<ProductType>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
