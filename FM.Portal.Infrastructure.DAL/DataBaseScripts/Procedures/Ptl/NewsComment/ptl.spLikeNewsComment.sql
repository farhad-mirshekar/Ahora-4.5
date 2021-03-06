USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spLikeNewsComment'))
	DROP PROCEDURE ptl.spLikeNewsComment
GO

CREATE PROCEDURE ptl.spLikeNewsComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Like INT;
	SET @Like = (SELECT LikeCount FROM ptl.NewsComment WHERE ID = @ID)
	SET @Like = @Like + 1;
	
	BEGIN TRAN

		INSERT INTO ptl.NewsComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			ptl.NewsComment
		SET 
			LikeCount = @Like 
		WHERE 
			ID = @ID
	COMMIT
	
	RETURN @@ROWCOUNT
END