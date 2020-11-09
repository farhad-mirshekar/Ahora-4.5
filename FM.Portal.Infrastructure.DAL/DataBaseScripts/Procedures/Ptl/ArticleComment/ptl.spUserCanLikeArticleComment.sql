USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spUserCanLikeArticleComment'))
	DROP PROCEDURE ptl.spUserCanLikeArticleComment
GO

CREATE PROCEDURE ptl.spUserCanLikeArticleComment
@CommentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Count INT;
	SET @Count = 
	(
		SELECT 
			COUNT(*)
		FROM 
			ptl.ArticleComment_User_Mapping
		WHERE 
			CommentID = @CommentID 
		AND	 UserID = @UserID
	)

	RETURN @Count
END