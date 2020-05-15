USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteProductVariantAttribute'))
	DROP PROCEDURE app.spDeleteProductVariantAttribute
GO

CREATE PROCEDURE app.spDeleteProductVariantAttribute
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM	[app].[ProductVariantAttributeValue] WHERE ID = @ID
END