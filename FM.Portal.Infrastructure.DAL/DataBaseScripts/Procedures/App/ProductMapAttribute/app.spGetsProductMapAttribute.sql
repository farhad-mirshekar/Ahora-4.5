USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductMapAttribute'))
	DROP PROCEDURE app.spGetsProductMapAttribute
GO

CREATE PROCEDURE app.spGetsProductMapAttribute
@ProductID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		pma.ID,
		pa.Name,
		pma.CreationDate
	FROM app.Product_ProductAttribute_Mapping pma
	INNER JOIN app.ProductAttribute pa ON pma.ProductAttributeID = pa.ID
	WHERE pma.ProductID = @ProductID
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END