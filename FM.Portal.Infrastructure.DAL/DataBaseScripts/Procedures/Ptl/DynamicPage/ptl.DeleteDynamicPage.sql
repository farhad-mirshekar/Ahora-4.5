USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spDeleteDynamicPage'))
	DROP PROCEDURE ptl.spDeleteDynamicPage
GO

CREATE PROCEDURE ptl.spDeleteDynamicPage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM ptl.DynamicPage
	WHERE ID = @ID
	RETURN @@ROWCOUNT

END