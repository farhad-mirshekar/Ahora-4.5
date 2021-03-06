USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductVariantAttributeValue'))
	DROP PROCEDURE app.spGetsProductVariantAttributeValue
GO

CREATE PROCEDURE app.spGetsProductVariantAttributeValue
@ProductVariantAttributeID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		value.*
	FROM app.ProductVariantAttributeValue value
	INNER JOIN 
	app.Product_ProductAttribute_Mapping map ON value.ProductVariantAttributeID = map.ID
	WHERE map.ID = @ProductVariantAttributeID
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END