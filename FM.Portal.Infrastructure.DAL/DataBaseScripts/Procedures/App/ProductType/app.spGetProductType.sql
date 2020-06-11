USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductType'))
	DROP PROCEDURE app.spGetProductType
GO

CREATE PROCEDURE app.spGetProductType
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		ProductType.*
	FROM	
		[app].[ProductType] ProductType
	WHERE 
		ProductType.ID = @ID
END