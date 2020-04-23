USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsPaymentForUser'))
	DROP PROCEDURE app.spGetsPaymentForUser
GO

CREATE PROCEDURE app.spGetsPaymentForUser
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		Payment.ID,
		CAST(payment.Price AS nvarchar) Price,
		Payment.CreationDate,
		orders.TrackingCode
	FROM	
		[app].[Payment] payment
	INNER JOIN
		[app].[Order] orders ON payment.OrderID = orders.ID
	WHERE 
		payment.TransactionStatus = 0 AND
		orders.UserID = @UserID
END