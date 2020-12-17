using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class SliderDataSource : ISliderDataSource
    {
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteSlider", param);
                    if (result > 0)
                        return Result.Successful();

                    return Result.Failure();
                }

            }
            catch (Exception) { return Result.Failure(); }
        }

        public Result<Slider> Get(Guid ID)
        {
            try
            {
                var obj = new Slider();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetSlider", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                        }
                    }

                }

                if (obj.ID != Guid.Empty)
                    return Result<Slider>.Successful(data: obj);

                return Result<Slider>.Successful(data: null);
            }
            catch (Exception e) { return Result<Slider>.Failure(); }
        }
        public Result<Slider> Insert(Slider model)
            => Modify(true, model);

        public DataTable List(SliderListVM listVM)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@Title", listVM?.Title);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[3] = new SqlParameter("@Enabled", (byte)listVM.Enabled);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsSlider", param);
            }
            catch (Exception e) { throw; }
        }
        public Result<Slider> Update(Slider model)
        => Modify(false, model);

        private Result<Slider> Modify(bool IsNewRecord, Slider model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[2] = new SqlParameter("@Priority", model.Priority);
                    param[3] = new SqlParameter("@Title", model.Title);
                    param[4] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[5] = new SqlParameter("@Url", model.Url);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifySlider", param);

                    if (result > 0)
                        return Get(model.ID);

                    return Result<Slider>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
