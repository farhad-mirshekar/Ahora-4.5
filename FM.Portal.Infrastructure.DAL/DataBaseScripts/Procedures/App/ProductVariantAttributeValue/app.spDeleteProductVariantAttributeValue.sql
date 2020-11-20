USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteProductVariantAttributeValue'))
	DROP PROCEDURE app.spDeleteProductVariantAttributeValue
GO

CREATE PROCEDURE app.spDeleteProductVariantAttributeValue
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM	[app].[ProductVariantAttributeValue] WHERE ID = @ID
	RETURN @@ROWCOUNT
END