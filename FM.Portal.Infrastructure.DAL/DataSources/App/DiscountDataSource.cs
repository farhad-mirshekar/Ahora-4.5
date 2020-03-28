using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class DiscountDataSource : IDiscountDataSource
    {

        private Result<Discount> Modify(bool isNewRecord , Discount model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Name", model.Name);
                    param[2] = new SqlParameter("@DiscountType", (byte)model.DiscountType);
                    param[3] = new SqlParameter("@DiscountPercentage", model.DiscountPercentage);
                    param[4] = new SqlParameter("@DiscountAmount", model.DiscountAmount);
                    param[5] = new SqlParameter("@isNewRecord", isNewRecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyDiscount", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { return Result<Discount>.Failure(message:e.ToString()); }
        }
        public Result<Discount> Get(Guid ID)
        {
            try
            {
                Discount obj = new Discount();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetDiscount", param))
                    {
                        while (dr.Read())
                        {
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.DiscountType =(DiscountType) SQLHelper.CheckIntNull(dr["DiscountType"]);
                            obj.DiscountPercentage = SQLHelper.CheckIntNull(dr["DiscountPercentage"]);
                            obj.DiscountAmount = SQLHelper.CheckDecimalNull(dr["DiscountAmount"]);
                        }
                    }

                }
                return Result<Discount>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Discount>.Failure(message:e.ToString()); }
        }

        public Result<Discount> Insert(Discount model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsDiscount", null);
            }
            catch (Exception e) { throw; }
        }

        public Result<Discount> Update(Discount model)
        {
            return Modify(false, model);
        }
    }
}
