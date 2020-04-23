USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetPaymentByToken'))
	DROP PROCEDURE app.spGetPaymentByToken
GO

CREATE PROCEDURE app.spGetPaymentByToken
@Token NVARCHAR(MAX),
@BankName TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		payment.*
	FROM	
		[app].[Payment] payment
	INNER JOIN
		[app].[Order] orders ON payment.OrderID = orders.ID
	INNER JOIN
		[app].[Bank] bank ON orders.BankID = bank.ID
	WHERE 
		payment.Token = LTRIM(RTRIM(@Token)) AND
		bank.BankName = @BankName
END