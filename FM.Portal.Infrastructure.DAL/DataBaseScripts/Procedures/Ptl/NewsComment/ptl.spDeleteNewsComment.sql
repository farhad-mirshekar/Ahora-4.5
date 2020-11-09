USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteNewsComment'))
	DROP PROCEDURE ptl.spDeleteNewsComment
GO

CREATE PROCEDURE ptl.spDeleteNewsComment
@RemoverID UNIQUEIDENTIFIER,
@CommentID UNIQUEIDENTIFIER
AS
BEGIN
	BEGIN TRAN
		UPDATE
		ptl.NewsComment
	SET
		RemoverID = @RemoverID
	WHERE
		ID = @CommentID

		UPDATE
		ptl.NewsComment
	SET
		RemoverID = @RemoverID
	WHERE
		ParentID = @CommentID
	COMMIT
	
	RETURN @@ROWCOUNT
END