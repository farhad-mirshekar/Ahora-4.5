using System;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;
using FM.Portal.Core.Result;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class FaqDataSource : IFaqDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public FaqDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result<FAQ> Insert(FAQ model)
        {
            model.ID = Guid.NewGuid();
           return Modify(true, model);
        }
        public Result<FAQ> Update(FAQ model)
        {
            return Modify(false,model);
        }
        public DataTable List(Guid FAQGroupID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@FAQGroupID", FAQGroupID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsFaq", param);
            }
            catch
            {
                throw;
            }
        }

        private Result<FAQ> Modify(bool isNewrecord, FAQ model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@CreatorID", _requestInfo.UserId);
                    param[2] = new SqlParameter("@FAQGroupID", model.FAQGroupID);
                    param[3] = new SqlParameter("@Answer", model.Answer);
                    param[4] = new SqlParameter("@Question", model.Question);
                    param[5] = new SqlParameter("@isNewRecord", isNewrecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyFaq", param);
                    return Get(model.ID);
                }
            }
            catch
            {
                return Result<FAQ>.Failure();
            }
        }

        public Result<FAQ> Get(Guid ID)
        {
            try
            {
                FAQ obj = new FAQ();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetFaqGroup", param))
                    {
                        while (dr.Read())
                        {
                            obj.Answer = SQLHelper.CheckStringNull(dr["Answer"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.CreatorID = SQLHelper.CheckGuidNull(dr["CreatorID"]);
                            obj.FAQGroupID = SQLHelper.CheckGuidNull(dr["FAQGroupID"]);
                            obj.Question= SQLHelper.CheckStringNull(dr["Question"]);

                        }
                    }

                }
                return Result<FAQ>.Successful(data: obj);
            }
            catch
            {
                return Result<FAQ>.Failure();
            }
        }
    }
}
