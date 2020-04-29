USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsPayment'))
	DROP PROCEDURE app.spGetsPayment
GO

CREATE PROCEDURE app.spGetsPayment
--WITH ENCRYPTION
AS
BEGIN
;WITH OrderDetail AS(
	SELECT
		detail.Quantity,
		detail.OrderID
	FROM 
		app.OrderDetail detail
)
	SELECT 
		payment.ID,
		payment.CreationDate,
		payment.TransactionStatus AS ResCode,
		payment.Price,
		CONCAT(buyer.FirstName , ' ' , buyer.LastName) AS BuyerInfo,
		COALESCE(buyer.CellPhone , N'شماره تلفن ثبت نشده است') AS BuyerPhone,
		bank.BankName,
		OrderDetail.Quantity CountBuy
	FROM 
		[app].[Payment] payment
	INNER JOIN
		[app].[order] orders ON payment.OrderID = orders.ID
	INNER JOIN
		[org].[User] buyer ON orders.UserID = buyer.ID
	INNER JOIN
		OrderDetail OrderDetail ON orders.ID = OrderDetail.OrderID
	LEFT JOIN
		[app].[Bank] bank ON orders.BankID = bank.ID
	WHERE 
		payment.TransactionStatus = 0
END

