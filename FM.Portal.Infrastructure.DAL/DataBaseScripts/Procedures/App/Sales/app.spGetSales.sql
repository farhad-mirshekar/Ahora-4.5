USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetSales'))
	DROP PROCEDURE app.spGetSales
GO

CREATE PROCEDURE app.spGetSales
@ID UNIQUEIDENTIFIER,
@PaymentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		Sales.*
	FROM	
		[app].[vwSales] Sales
	WHERE 
		(@ID IS NULL OR Sales.ID = @ID)
	AND (@PaymentID IS NULL OR Sales.PaymentID = @PaymentID)
END