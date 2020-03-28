USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spListAttributeForProduct'))
	DROP PROCEDURE app.spListAttributeForProduct
GO

CREATE PROCEDURE app.spListAttributeForProduct
@ProductID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT
	 
		map.ID,
		map.ProductID,
		map.ProductAttributeID AS AttributeID,
		map.TextPrompt,
		map.AttributeControlType,
		map.IsRequired,
		map.CreationDate,
		attribute.Name

	FROM app.Product product

	INNER JOIN app.Product_ProductAttribute_Mapping map ON product.ID = map.ProductID
	INNER JOIN app.ProductAttribute attribute ON map.ProductAttributeID = attribute.ID

	WHERE product.ID = @ProductID
END