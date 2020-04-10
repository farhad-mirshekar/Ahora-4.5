USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsPayment'))
	DROP PROCEDURE app.spGetsPayment
GO

CREATE PROCEDURE app.spGetsPayment
@ResCode TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		payment.ID,
		payment.CreationDate,
		Payment.TransactionStatus AS ResCode,
		CONCAT(buyer.FirstName , ' ' , buyer.LastName) AS BuyerInfo,
		COALESCE(buyer.CellPhone , N'شماره تلفن ثبت نشده است') AS BuyerPhone
	FROM 
		[app].[Payment] payment
	INNER JOIN
		[app].[order] orders ON payment.OrderID = orders.ID
	INNER JOIN
		[org].[User] buyer ON orders.UserID = buyer.ID
	WHERE 
		payment.TransactionStatus = @ResCode
END

