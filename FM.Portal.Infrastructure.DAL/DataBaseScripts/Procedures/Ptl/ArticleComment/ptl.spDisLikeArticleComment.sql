USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDisLikeArticleComment'))
	DROP PROCEDURE ptl.spDisLikeArticleComment
GO

CREATE PROCEDURE ptl.spDisLikeArticleComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @DisLike INT;
	SET @DisLike = (SELECT DisLikeCount FROM ptl.ArticleComment WHERE ID = @ID)
	SET @DisLike = @DisLike + 1;

	BEGIN TRAN
		INSERT INTO ptl.ArticleComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			ptl.ArticleComment
		SET 
			DisLikeCount = @DisLike 
		WHERE 
			ID = @ID
	COMMIT

	SELECT @DisLike
END