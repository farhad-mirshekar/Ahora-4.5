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
   public class FaqGroupDataSource :  IFaqGroupDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public FaqGroupDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
       public DataTable List()
        {
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsFaqGroup", null);
        }
        private Result<FAQGroup> Modify(bool isNewrecord, FAQGroup model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@CreatorID", _requestInfo.UserId);
                    param[2] = new SqlParameter("@ApplicationID", _requestInfo.ApplicationId);
                    param[3] = new SqlParameter("@Title", model.Title);
                    param[4] = new SqlParameter("@isNewRecord", isNewrecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyFaqGroup", param);

                    return Get(model.ID);
                }
            }
            catch { return Result<FAQGroup>.Failure(); }
        }

        public Result<FAQGroup> Insert(FAQGroup model)
        {
            model.ID = Guid.NewGuid();
           return Modify(true, model);
        }
        public Result<FAQGroup> Update(FAQGroup model)
        {
           return Modify(false, model);
        }

        public Result<FAQGroup> Get(Guid ID)
        {
            try
            {
                FAQGroup obj = new FAQGroup();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetFaqGroup", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreatorID = SQLHelper.CheckGuidNull(dr["CreatorID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.ApplicationID = SQLHelper.CheckGuidNull(dr["ApplicationID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                        }
                    }

                }
                return Result<FAQGroup>.Successful(data:obj);
            }
            catch
            {
                return Result<FAQGroup>.Failure();
            }
        }

        public DataTable ListForWeb()
        {
            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spListFaqGroup", null);
        }
    }
}
