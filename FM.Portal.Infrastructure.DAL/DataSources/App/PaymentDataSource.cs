using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Data.SqlClient;
using FM.Portal.Core.Result;
using System.Collections.Generic;

namespace FM.Portal.Infrastructure.DAL
{
    public class PaymentDataSource : IPaymentDataSource
    {
        public Result FirstStepPayment(Order order, OrderDetail detail, Payment payment)
        {
            try
            {
                order.ID = Guid.NewGuid();
                payment.ID = Guid.NewGuid();
                detail.ID = Guid.NewGuid();
                payment.OrderID = order.ID;
                detail.OrderID = order.ID;

                var commands = new List<SqlCommand>();

                #region Order
                SqlParameter[] paramOrder = new SqlParameter[9];
                paramOrder[0] = new SqlParameter("@ID", order.ID);

                paramOrder[1] = new SqlParameter("@AddressID", order.AddressID);
                paramOrder[2] = new SqlParameter("@BankID", order.BankID);
                paramOrder[3] = new SqlParameter("@Price", order.Price);
                paramOrder[4] = new SqlParameter("@SendType", (byte)order.SendType);
                paramOrder[5] = new SqlParameter("@ShoppingID", order.ShoppingID);
                paramOrder[6] = new SqlParameter("@UserID", order.UserID);
                paramOrder[7] = new SqlParameter("@IsNewRecord", true);
                paramOrder[8] = new SqlParameter("@TrackingCode", "");
                commands.Add(SQLHelper.CreateCommand("app.spModifyOrder", CommandType.StoredProcedure, paramOrder));
                #endregion
                #region OrderDetail
                SqlParameter[] paramDetail = new SqlParameter[7];
                paramDetail[0] = new SqlParameter("@ID", detail.ID);

                paramDetail[1] = new SqlParameter("@AttributeJson", detail.AttributeJson);
                paramDetail[2] = new SqlParameter("@OrderID", detail.OrderID);
                paramDetail[3] = new SqlParameter("@ProductJson", detail.ProductJson);
                paramDetail[4] = new SqlParameter("@UserJson", detail.UserJson);
                paramDetail[5] = new SqlParameter("@ShoppingCartJson", detail.ShoppingCartJson);
                paramDetail[6] = new SqlParameter("@Quantity", detail.Quantity);

                commands.Add(SQLHelper.CreateCommand("app.spModifyOrderDetail", CommandType.StoredProcedure, paramDetail));
                #endregion
                #region Payment
                SqlParameter[] paramPayment = new SqlParameter[5];
                paramPayment[0] = new SqlParameter("@ID", payment.ID);

                paramPayment[1] = new SqlParameter("@OrderID", payment.OrderID);
                paramPayment[2] = new SqlParameter("@Price", payment.Price);
                paramPayment[3] = new SqlParameter("@Token", payment.Token);
                paramPayment[4] = new SqlParameter("@UserID", payment.UserID);
                commands.Add(SQLHelper.CreateCommand("app.spInsertPayment", CommandType.StoredProcedure, paramPayment));
                #endregion
                SQLHelper.BatchExcute(commands.ToArray());
                return Result.Successful();
            }
            catch { return Result.Failure(); }
        }

        public Result<Payment> Get(Guid ID)
        {
            try
            {
                var obj = new Payment();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetPayment", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.OrderID = SQLHelper.CheckGuidNull(dr["OrderID"]);
                            obj.RetrivalRefNo = SQLHelper.CheckStringNull(dr["RetrivalRefNo"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.SystemTraceNo = SQLHelper.CheckStringNull(dr["SystemTraceNo"]);
                            obj.TransactionStatus = SQLHelper.CheckIntNull(dr["UserID"]);
                            obj.TransactionStatusMessage = SQLHelper.CheckStringNull(dr["TransactionStatusMessage"]);
                            obj.Token = SQLHelper.CheckStringNull(dr["Token"]);
                        }
                    }

                }
                return Result<Payment>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Payment>.Failure(); }
        }

        public Result<Payment> GetByShoppingID(Guid ShoppingID)
        {
            try
            {
                var obj = new Payment();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ShoppingID", ShoppingID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetPaymentByShoppingID", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.OrderID = SQLHelper.CheckGuidNull(dr["OrderID"]);
                            obj.RetrivalRefNo = SQLHelper.CheckStringNull(dr["RetrivalRefNo"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.SystemTraceNo = SQLHelper.CheckStringNull(dr["SystemTraceNo"]);
                            obj.TransactionStatus = SQLHelper.CheckIntNull(dr["TransactionStatus"]);
                            obj.TransactionStatusMessage = SQLHelper.CheckStringNull(dr["TransactionStatusMessage"]);
                            obj.Token = SQLHelper.CheckStringNull(dr["Token"]);
                        }
                    }

                }
                return Result<Payment>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Payment>.Failure(); }
        }

        public Result<Payment> GetByToken(string Token , BankName BankName)
        {
            try
            {
                var obj = new Payment();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Token", Token);
                param[1] = new SqlParameter("@BankName", (byte)BankName);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetPaymentByToken", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Price = SQLHelper.CheckDecimalNull(dr["Price"]);
                            obj.OrderID = SQLHelper.CheckGuidNull(dr["OrderID"]);
                            obj.RetrivalRefNo = SQLHelper.CheckStringNull(dr["RetrivalRefNo"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.SystemTraceNo = SQLHelper.CheckStringNull(dr["SystemTraceNo"]);
                            obj.TransactionStatus = SQLHelper.CheckIntNull(dr["UserID"]);
                            obj.TransactionStatusMessage = SQLHelper.CheckStringNull(dr["TransactionStatusMessage"]);
                            obj.Token = SQLHelper.CheckStringNull(dr["Token"]);
                        }
                    }

                }
                return Result<Payment>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Payment>.Failure(); }
        }

        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsPayment", null);
            }
            catch
            {
                throw;
            }
        }

        public DataTable ListPaymentForUser(PaymentListForUserVM listVm)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@UserID", listVm.UserID);
                param[1] = new SqlParameter("@PageSize", listVm.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVm.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsPaymentForUser", param);
            }
            catch
            {
                throw;
            }
        }

        public Result<Payment> Update(Payment model)
        => Modify(false, model);
        private Result<Payment> Modify(bool IsNewRecord, Payment model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@RetrivalRefNo", model.RetrivalRefNo);
                    param[2] = new SqlParameter("@SystemTraceNo", model.SystemTraceNo);
                    param[3] = new SqlParameter("@TransactionStatusMessage", model.TransactionStatusMessage);
                    param[4] = new SqlParameter("@TransactionStatus", model.TransactionStatus);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyPayment", param);

                    return Get(model.ID);
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
