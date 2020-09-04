using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class SalesDataSource : ISalesDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public SalesDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public Result<Sales> Get(Guid? ID , Guid? PaymentID)
        {
            try
            {
                var obj = new Sales();
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                param[0].Value = ID.HasValue ? ID.Value : (object)DBNull.Value;

                param[1] = new SqlParameter("@PaymentID", SqlDbType.UniqueIdentifier);
                param[1].Value = PaymentID.HasValue ? PaymentID.Value : (object)DBNull.Value;


                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetSales", param))
                    {
                        while (dr.Read())
                        {
                            obj.PaymentID = SQLHelper.CheckGuidNull(dr["PaymentID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.RemoverDate = SQLHelper.CheckDateTimeNull(dr["RemoveDate"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.BuyerInfo = SQLHelper.CheckStringNull(dr["BuyerInfo"]);
                            obj.BuyerPhone = SQLHelper.CheckStringNull(dr["BuyerPhone"]);
                            obj.LastDocState =(SalesDocState) SQLHelper.CheckByteNull(dr["LastDocState"]);
                            obj.LastFlowDate = SQLHelper.CheckDateTimeNull(dr["LastFlowDate"]);
                            obj.LastFromDocState = (SalesDocState)SQLHelper.CheckByteNull(dr["LastFromDocState"]);
                            obj.LastFromPositionID = SQLHelper.CheckGuidNull(dr["LastFromPositionID"]);
                            obj.LastFromUserID = SQLHelper.CheckGuidNull(dr["LastFromUserID"]);
                            //obj.LastFromUserName = SQLHelper.CheckStringNull(dr["LastFromUserName"]);
                            obj.LastReadDate = SQLHelper.CheckDateTimeNull(dr["LastReadDate"]);
                            obj.LastSendType =(SendDocumentType) SQLHelper.CheckByteNull(dr["LastSendType"]);
                            obj.LastToPositionID = SQLHelper.CheckGuidNull(dr["LastToPositionID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.TransactionStatusMessage = SQLHelper.CheckStringNull(dr["TransactionStatusMessage"]);
                        }
                    }

                }
                return Result<Sales>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Sales>.Failure(); }
        }

        public Result<Sales> Insert(Sales model)
        => Modify(true, model);

        public DataTable List(SalesListVM listVM)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@UserPositionID",_requestInfo.PositionId);
                param[1] = new SqlParameter("@ActionState", (byte)listVM.ActionState);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsSales",param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Sales> Update(Sales model)
        =>Modify(false,model);

        #region Private
        private Result<Sales> Modify(bool IsNewRecord , Sales model)
        {
            try
            {
                var commands = new List<SqlCommand>();
                
                var baseDocumentParam = new SqlParameter[2];
                baseDocumentParam[0] = new SqlParameter("@ID", model.ID);
                baseDocumentParam[1] = new SqlParameter("@IsNewRecord", IsNewRecord);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyBaseDocument", CommandType.StoredProcedure, baseDocumentParam));

                var salesParam = new SqlParameter[4];
                salesParam[0] = new SqlParameter("@ID", model.ID);
                salesParam[1] = new SqlParameter("@PaymentID", model.PaymentID);
                salesParam[2] = new SqlParameter("@IsNewRecord", IsNewRecord);
                salesParam[3] = new SqlParameter("@Type", (byte)model.Type);

                commands.Add(SQLHelper.CreateCommand("app.spModifySales", CommandType.StoredProcedure, salesParam));

                SQLHelper.BatchExcute(commands.ToArray());
                return Get(model.ID, null);
            }
            catch(Exception e) { throw; }
        }
        #endregion
    }
}
