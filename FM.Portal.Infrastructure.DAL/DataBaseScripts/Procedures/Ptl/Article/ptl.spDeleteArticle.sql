USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteArticle'))
	DROP PROCEDURE ptl.spDeleteArticle
GO

CREATE PROCEDURE ptl.spDeleteArticle
@ID UNIQUEIDENTIFIER,
@RemoverID UNIQUEIDENTIFIER
AS
BEGIN
	--DELETE FROM [ptl].[Article] WHERE ID = @ID
	UPDATE ptl.Article
	SET
		RemoverID = @RemoverID,
		RemoverDate = GETDATE()
	WHERE
		ID = @ID
	RETURN @@ROWCOUNT
END