
USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('app.spDeleteRelatedProduct'))
	DROP PROCEDURE app.spDeleteRelatedProduct
GO

CREATE PROCEDURE app.spDeleteRelatedProduct
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM app.RelatedProduct WHERE ID = @ID
END