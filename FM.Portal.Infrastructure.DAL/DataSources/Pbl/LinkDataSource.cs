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
    public class LinkDataSource : ILinkDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public LinkDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result<Link> Insert(Link model)
        => Modify(true, model);

        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteLink", param);
                    if (result > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Link> Get(Guid ID)
        {
            try
            {
                var obj = new Link();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetLink", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.IconText = SQLHelper.CheckStringNull(dr["IconText"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                            obj.ShowFooter = SQLHelper.CheckBoolNull(dr["ShowFooter"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                        }
                    }

                }
                return Result<Link>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Link>.Failure(); }
        }

        public DataTable List(LinkListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ShowFooter", SqlDbType.Bit);
                param[0].Value = listVM.ShowFooter ?? (object)DBNull.Value;
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsLink", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Link> Update(Link model)
        => Modify(false, model);
        private Result<Link> Modify(bool IsNewrecord, Link model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[10];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@isNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@Description", model.Description);
                    param[3] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[4] = new SqlParameter("@IconText", model.IconText);
                    param[5] = new SqlParameter("@Priority", model.Priority);
                    param[6] = new SqlParameter("@ShowFooter", model.ShowFooter);
                    param[7] = new SqlParameter("@Url", model.Url);
                    param[8] = new SqlParameter("@UserId", _requestInfo.UserId);
                    param[9] = new SqlParameter("@Name", model.Name);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyLink", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<Link>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
