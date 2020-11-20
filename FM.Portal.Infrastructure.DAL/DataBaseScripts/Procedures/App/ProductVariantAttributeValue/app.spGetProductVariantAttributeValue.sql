USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductVariantAttributeValue'))
	DROP PROCEDURE app.spGetProductVariantAttributeValue
GO

CREATE PROCEDURE app.spGetProductVariantAttributeValue
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		attr.*
	FROM	
		[app].[ProductVariantAttributeValue] attr
	WHERE 
		attr.ID = @ID
END