USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetSales'))
	DROP PROCEDURE app.spGetSales
GO

CREATE PROCEDURE app.spGetSales
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		Sales.*
	FROM	
		[app].[Sales] Sales
	WHERE 
		Sales.ID = @ID
END