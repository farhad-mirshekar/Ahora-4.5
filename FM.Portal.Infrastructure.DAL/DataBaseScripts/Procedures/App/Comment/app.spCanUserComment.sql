USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spCanUserComment'))
	DROP PROCEDURE app.spCanUserComment
GO

CREATE PROCEDURE app.spCanUserComment
@DocumentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		TOP 1
		comment.*
	FROM	
		[app].[Comment] comment
	WHERE 
		comment.DocumentID = @DocumentID AND
		comment.UserID = @UserID AND
		comment.CommentType = 1
	ORDER BY
		comment.CreationDate DESC
END