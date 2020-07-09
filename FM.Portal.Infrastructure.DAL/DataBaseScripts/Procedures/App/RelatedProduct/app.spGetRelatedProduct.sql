USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetRelatedProduct'))
	DROP PROCEDURE app.spGetRelatedProduct
GO

CREATE PROCEDURE app.spGetRelatedProduct
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		RelatedProduct.*
	FROM	
		[app].[RelatedProduct] RelatedProduct
	WHERE 
		RelatedProduct.ID = @ID
END