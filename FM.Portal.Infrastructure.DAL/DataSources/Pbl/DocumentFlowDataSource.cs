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
    public class DocumentFlowDataSource : IDocumentFlowDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public DocumentFlowDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        public Result Insert(DocumentFlow flow)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[7];
                    param[0] = new SqlParameter("@Comment", flow.Comment);
                    param[1] = new SqlParameter("@DocumentID", flow.DocumentID);
                    param[2] = new SqlParameter("@FromDocState", (byte)flow.FromDocState);
                    param[3] = new SqlParameter("@FromPositionID", flow.FromPositionID);
                    param[4] = new SqlParameter("@ToDocState", (byte)flow.ToDocState);
                    param[5] = new SqlParameter("@ToPositionID", flow.ToPositionID);
                    param[6] = new SqlParameter("@SendType", (byte)flow.SendType);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spInsertDocumentFlow", param);
                    if (result > 0)
                        return Result.Successful();
                    return Result.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(DocumentFlowListVM listVM)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@DocumentID",listVM.DocumentID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsDocumentFlow", param);
            }
            catch(Exception e) { throw; }
        }
    }
}
