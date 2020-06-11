USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('app.spDeleteProductType'))
	DROP PROCEDURE app.spDeleteProductType
GO

CREATE PROCEDURE app.spDeleteProductType
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM app.ProductType WHERE ID = @ID
END