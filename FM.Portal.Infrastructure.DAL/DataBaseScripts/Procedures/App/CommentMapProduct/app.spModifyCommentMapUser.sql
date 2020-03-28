USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyCommentMapUser'))
	DROP PROCEDURE app.spModifyCommentMapUser
GO

CREATE PROCEDURE app.spModifyCommentMapUser
@CommentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	INSERT INTO app.Comment_User_Mapping (CommentID , UserID , CreationDate)
	VALUES ( @CommentID , @UserID , GETDATE())
	
END