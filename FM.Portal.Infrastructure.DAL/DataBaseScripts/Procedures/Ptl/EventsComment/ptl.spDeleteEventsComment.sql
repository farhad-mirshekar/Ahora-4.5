USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteEventsComment'))
	DROP PROCEDURE ptl.spDeleteEventsComment
GO

CREATE PROCEDURE ptl.spDeleteEventsComment
@RemoverID UNIQUEIDENTIFIER,
@CommentID UNIQUEIDENTIFIER
AS
BEGIN
	BEGIN TRAN
		UPDATE
		ptl.EventsComment
	SET
		RemoverID = @RemoverID
	WHERE
		ID = @CommentID

		UPDATE
		ptl.EventsComment
	SET
		RemoverID = @RemoverID
	WHERE
		ParentID = @CommentID
	COMMIT
	
	RETURN @@ROWCOUNT
END