USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDisLikeNewsComment'))
	DROP PROCEDURE ptl.spDisLikeNewsComment
GO

CREATE PROCEDURE ptl.spDisLikeNewsComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @DisLike INT;
	SET @DisLike = (SELECT DisLikeCount FROM ptl.NewsComment WHERE ID = @ID)
	SET @DisLike = @DisLike + 1;

	BEGIN TRAN
		INSERT INTO ptl.NewsComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			ptl.NewsComment
		SET 
			DisLikeCount = @DisLike 
		WHERE 
			ID = @ID
	COMMIT

	SELECT @DisLike
END