USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteArticleComment'))
	DROP PROCEDURE ptl.spDeleteArticleComment
GO

CREATE PROCEDURE ptl.spDeleteArticleComment
@RemoverID UNIQUEIDENTIFIER,
@CommentID UNIQUEIDENTIFIER
AS
BEGIN
	BEGIN TRAN
		UPDATE
		ptl.ArticleComment
	SET
		RemoverID = @RemoverID
	WHERE
		ID = @CommentID

		UPDATE
		ptl.ArticleComment
	SET
		RemoverID = @RemoverID
	WHERE
		ParentID = @CommentID
	COMMIT
	
	RETURN @@ROWCOUNT
END