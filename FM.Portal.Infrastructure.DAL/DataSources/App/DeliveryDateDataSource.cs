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
   public class DeliveryDateDataSource : IDeliveryDateDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public DeliveryDateDataSource(IRequestInfo requestInfo)
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
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDeleteDeliveryDate", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<DeliveryDate> Get(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                var obj = new DeliveryDate();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetDeliveryDate", param))
                    {
                        while (dr.Read())
                        {
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                        }
                    }

                }
                return Result<DeliveryDate>.Successful(data: obj);
            }
            catch
            {
                return Result<DeliveryDate>.Failure();
            }
        }

        public Result<DeliveryDate> Insert(DeliveryDate model)
        => Modify(true, model);

        public DataTable List(DeliveryDateListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@Enabled", SqlDbType.TinyInt);
                param[0].Value = listVM.Enabled != EnableMenuType.نامشخص ? listVM.Enabled : (object)DBNull.Value;

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsDeliveryDate", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<DeliveryDate> Update(DeliveryDate model)
        => Modify(false, model);

        private Result<DeliveryDate> Modify(bool IsNewRecord, DeliveryDate model)
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
                    param[5] = new SqlParameter("@Name", model.Name);
                    param[6] = new SqlParameter("@Priority", model.Priority);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyDeliveryDate", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<DeliveryDate>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
