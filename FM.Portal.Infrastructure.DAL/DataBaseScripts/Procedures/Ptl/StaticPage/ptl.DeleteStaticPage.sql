USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spDeleteStaticPage'))
	DROP PROCEDURE ptl.spDeleteStaticPage
GO

CREATE PROCEDURE ptl.spDeleteStaticPage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM ptl.Pages
	WHERE ID = @ID

	DELETE FROM ptl.StaticPage
	WHERE ID = @ID
	RETURN @@ROWCOUNT

END