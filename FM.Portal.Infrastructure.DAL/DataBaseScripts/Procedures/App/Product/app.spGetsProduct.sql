USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProduct'))
	DROP PROCEDURE app.spGetsProduct
GO

CREATE PROCEDURE app.spGetsProduct
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		product.*,
		category.Title AS CategoryName
	FROM app.Product product
	INNER JOIN app.Category category ON product.CategoryID = category.ID
	ORDER BY product.CreationDate DESC
END