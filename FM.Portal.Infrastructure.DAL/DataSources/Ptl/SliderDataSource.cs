using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
   public class SliderDataSource : ISliderDataSource
    {
        public Result<int> Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteSlider", param);
                    return Result<int>.Successful(data: result);
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<Slider> Get(Guid ID)
        {
            try
            {
                var obj = new Slider();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetSlider", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.Deleted = SQLHelper.CheckBoolNull(dr["Deleted"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                        }
                    }

                }
                return Result<Slider>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Slider>.Failure(); }
        }
        public Result<Slider> Insert(Slider model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsSlider", null);
            }
            catch (Exception e) { throw; }
        }
        public DataTable List(int count)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                count = count == 0 ? 4 : count;
                param[0] = new SqlParameter("@count", count);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsSliderByCount", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Slider> Update(Slider model)
        {
            return Modify(false, model);
        }
        private Result<Slider> Modify(bool IsNewRecord, Slider model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    lock (con)
                    {
                        SqlParameter[] param = new SqlParameter[6];
                        param[0] = new SqlParameter("@ID", model.ID);
                        param[1] = new SqlParameter("@Enabled", (byte)model.Enabled);
                        param[2] = new SqlParameter("@Priority", model.Priority);
                        param[3] = new SqlParameter("@Title", model.Title);
                        param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);
                        param[5] = new SqlParameter("@Url", model.Url);

                        SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifySlider", param);

                        return Get(model.ID);
                    }
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
