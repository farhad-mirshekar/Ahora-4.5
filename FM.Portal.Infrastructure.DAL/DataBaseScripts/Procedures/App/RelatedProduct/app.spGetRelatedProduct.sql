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
		RelatedProduct.*,
		p2.Name AS ProductName2
	FROM	
		[app].[RelatedProduct] RelatedProduct
	INNER JOIN 
		app.Product p2 ON RelatedProduct.ProductID2 = p2.ID
	WHERE 
		RelatedProduct.ID = @ID
END