USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spListVarientForProduct'))
	DROP PROCEDURE app.spListVarientForProduct
GO

CREATE PROCEDURE app.spListVarientForProduct
@AttributeID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		*
	FROM app.ProductVariantAttributeValue 

	WHERE ProductVariantAttributeID = @AttributeID
END