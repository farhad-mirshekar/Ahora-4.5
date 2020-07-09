USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsRelatedProduct'))
	DROP PROCEDURE app.spGetsRelatedProduct
GO

CREATE PROCEDURE app.spGetsRelatedProduct
@ProductID1 UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		relatedProduct.*,
		p1.[Name] AS ProductName1 ,
		p2.[Name] AS ProductName2
	FROM app.relatedProduct relatedProduct
	INNER JOIN app.Product p1 ON relatedProduct.ProductID1 = p1.ID
	INNER JOIN app.Product p2 ON relatedProduct.ProductID2 = p2.ID
	WHERE
		(@ProductID1 IS NULL OR relatedProduct.ProductID1 = @ProductID1)
	ORDER BY relatedProduct.[Priority]
END