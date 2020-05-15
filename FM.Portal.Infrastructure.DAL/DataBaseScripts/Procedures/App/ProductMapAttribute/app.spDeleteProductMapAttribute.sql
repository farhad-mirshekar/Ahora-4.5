USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteProductMapAttribute'))
	DROP PROCEDURE app.spDeleteProductMapAttribute
GO

CREATE PROCEDURE app.spDeleteProductMapAttribute
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET XACT_ABORT ON;
BEGIN TRY
		BEGIN TRAN
	DELETE FROM	[app].[ProductVariantAttributeValue] WHERE ProductVariantAttributeID = @ID
	DELETE FROM	[app].[Product_ProductAttribute_Mapping] WHERE ID = @ID
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END