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
    public class ShippingCostDataSource : IShippingCostDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public ShippingCostDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteShippingCost", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<ShippingCost> Get(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                var obj = new ShippingCost();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetShippingCost", param))
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
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                        }
                    }

                }
                return Result<ShippingCost>.Successful(data: obj);
            }
            catch
            {
                return Result<ShippingCost>.Failure();
            }
        }

        public Result<ShippingCost> Insert(ShippingCost model)
        => Modify(true, model);

        public DataTable List(ShippingCostListVM listVM)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@Enabled", SqlDbType.TinyInt);
                param[0].Value = listVM.Enabled != EnableMenuType.نامشخص ? listVM.Enabled : (object)DBNull.Value;
                param[1] = new SqlParameter("@Priority", listVM.Priority);
                param[2] = new SqlParameter("@Name", listVM.Name);
                param[3] = new SqlParameter("@PageSize", listVM.PageSize);
                param[4] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsShippingCost", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<ShippingCost> Update(ShippingCost model)
        => Modify(false, model);

        private Result<ShippingCost> Modify(bool IsNewRecord, ShippingCost model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Description", model.Description);
                    param[2] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[3] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[4] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[5] = new SqlParameter("@Price", model.Price);
                    param[6] = new SqlParameter("@Name", model.Name);
                    param[7] = new SqlParameter("@Priority", model.Priority);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyShippingCost", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<ShippingCost>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
