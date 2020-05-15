USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductVariantAttribute'))
	DROP PROCEDURE app.spGetProductVariantAttribute
GO

CREATE PROCEDURE app.spGetProductVariantAttribute
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