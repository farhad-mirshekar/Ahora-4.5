USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetPaymentByShoppingID'))
	DROP PROCEDURE app.spGetPaymentByShoppingID
GO

CREATE PROCEDURE app.spGetPaymentByShoppingID
@ShoppingID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		payment.*
	FROM	
		[app].[Payment] payment
	INNER JOIN
		[app].[Order] orders ON payment.OrderID = orders.ID
	WHERE 
		orders.ShoppingID = @ShoppingID
END