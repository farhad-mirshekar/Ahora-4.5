USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCommentMapUser'))
	DROP PROCEDURE app.spGetCommentMapUser
GO

CREATE PROCEDURE app.spGetCommentMapUser
@CommentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT * FROM app.Comment_User_Mapping 
	WHERE 
		 CommentID = @CommentID AND
		 UserID = @UserID
	
END