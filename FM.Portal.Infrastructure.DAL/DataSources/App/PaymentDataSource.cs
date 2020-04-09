﻿using System;
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
                SqlParameter[] paramOrder = new SqlParameter[7];
                paramOrder[0] = new SqlParameter("@ID", order.ID);

                paramOrder[1] = new SqlParameter("@AddressID", order.AddressID);
                paramOrder[2] = new SqlParameter("@BankID", order.BankID);
                paramOrder[3] = new SqlParameter("@Price", order.Price);
                paramOrder[4] = new SqlParameter("@SendType", (byte)order.SendType);
                paramOrder[5] = new SqlParameter("@ShoppingID", order.ShoppingID);
                paramOrder[6] = new SqlParameter("@UserID", order.UserID);
                commands.Add(SQLHelper.CreateCommand("app.spModifyOrder", CommandType.StoredProcedure, paramOrder));
                #endregion
                #region OrderDetail
                SqlParameter[] paramDetail = new SqlParameter[5];
                paramDetail[0] = new SqlParameter("@ID", detail.ID);

                paramDetail[1] = new SqlParameter("@AttributeJson", detail.AttributeJson);
                paramDetail[2] = new SqlParameter("@OrderID", detail.OrderID);
                paramDetail[3] = new SqlParameter("@ProductJson", detail.ProductJson);
                paramDetail[4] = new SqlParameter("@UserJson", detail.UserJson);
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

        public Result<Payment> Get(Guid? ID, Guid? OrderID, Guid? UserID)
        {
            throw new NotImplementedException();
        }

        public Result<Payment> Update(Payment model)
        {
            throw new NotImplementedException();
        }
        private Result<Payment> Modify(bool IsNewRecord , Payment model)
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
                    param[4] = new SqlParameter("@TransactionStatus", (byte)model.TransactionStatus);
                    param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyPayment", param);

                    return Get(model.ID,null , null);
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
