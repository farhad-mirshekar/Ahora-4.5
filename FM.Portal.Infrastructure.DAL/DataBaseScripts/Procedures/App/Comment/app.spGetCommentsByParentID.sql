USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCommentsByParentID'))
	DROP PROCEDURE app.spGetCommentsByParentID
GO

CREATE PROCEDURE app.spGetCommentsByParentID
@ParentID UNIQUEIDENTIFIER,
@DocumentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		comment.*
	FROM	
		[app].[Comment] comment
	WHERE 
		comment.ParentID = @ParentID AND
		comment.DocumentID = @DocumentID
END