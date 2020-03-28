USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteArticle'))
	DROP PROCEDURE ptl.spDeleteArticle
GO

CREATE PROCEDURE ptl.spDeleteArticle
@ID UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM [ptl].[Article] WHERE ID = @ID
	RETURN @@ROWCOUNT
END