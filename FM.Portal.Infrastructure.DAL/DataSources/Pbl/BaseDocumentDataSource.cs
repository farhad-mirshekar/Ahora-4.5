using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class BaseDocumentDataSource : IBaseDocumentDataSource
    {
        public Result<BaseDocument> Get(Guid ID)
        {
            try
            {
                var obj = new BaseDocument();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetBaseDocument", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.PaymentID = SQLHelper.CheckGuidNull(dr["PaymentID"]);
                            obj.RemoverDate = SQLHelper.CheckDateTimeNull(dr["RemoverDate"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.Type = (DocumentType)SQLHelper.CheckByteNull(dr["Type"]);
                        }
                    }

                }
                return Result<BaseDocument>.Successful(data: obj);
            }
            catch (Exception e) { return Result<BaseDocument>.Failure(); }
        }

        public Result<BaseDocument> Insert(BaseDocument model)
        => Modify(true, model);

        public Result<BaseDocument> Update(BaseDocument model)
        => Modify(false, model);

        private Result<BaseDocument> Modify(bool IsNewrecord, BaseDocument model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[4];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@isNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@PaymentID", model.PaymentID);
                    param[3] = new SqlParameter("@Type", (byte)model.Type);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyBaseDocument", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<BaseDocument>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
    }
}
