USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsPayment'))
	DROP PROCEDURE app.spGetsPayment
GO

CREATE PROCEDURE app.spGetsPayment
--WITH ENCRYPTION
AS
BEGIN
;WITH Cart AS(
	SELECT
		COUNT(*) AS CountBuy,
		cart.ShoppingID,
		cart.UserID
	FROM 
		app.ShoppingCartItem cart
	GROUP BY
		cart.ShoppingID,
		cart.UserID
)
	SELECT 
		payment.ID,
		payment.CreationDate,
		Payment.TransactionStatus AS ResCode,
		CONCAT(buyer.FirstName , ' ' , buyer.LastName) AS BuyerInfo,
		COALESCE(buyer.CellPhone , N'شماره تلفن ثبت نشده است') AS BuyerPhone,
		bank.BankName,
		cart.CountBuy
	FROM 
		[app].[Payment] payment
	INNER JOIN
		[app].[order] orders ON payment.OrderID = orders.ID
	INNER JOIN
		[org].[User] buyer ON orders.UserID = buyer.ID
	INNER JOIN
		Cart cart ON orders.ShoppingID = cart.ShoppingID AND buyer.ID = cart.UserID
	LEFT JOIN
		[app].[Bank] bank ON orders.BankID = bank.ID
	WHERE 
		payment.TransactionStatus = 0
END

