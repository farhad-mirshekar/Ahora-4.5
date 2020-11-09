USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteProductComment'))
	DROP PROCEDURE app.spDeleteProductComment
GO

CREATE PROCEDURE app.spDeleteProductComment
@RemoverID UNIQUEIDENTIFIER,
@CommentID UNIQUEIDENTIFIER
AS
BEGIN
	BEGIN TRAN
		UPDATE
		app.ProductComment
	SET
		RemoverID = @RemoverID
	WHERE
		ID = @CommentID

		UPDATE
		app.ProductComment
	SET
		RemoverID = @RemoverID
	WHERE
		ParentID = @CommentID
	COMMIT
	
	RETURN @@ROWCOUNT
END