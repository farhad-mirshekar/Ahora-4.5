USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductMapAttribute'))
	DROP PROCEDURE app.spGetProductMapAttribute
GO

CREATE PROCEDURE app.spGetProductMapAttribute
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		attr.*
	FROM	
		[app].[Product_ProductAttribute_Mapping] attr
	WHERE 
		attr.ID = @ID
END