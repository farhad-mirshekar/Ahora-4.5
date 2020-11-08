USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetArticleComment'))
	DROP PROCEDURE ptl.spGetArticleComment
GO

CREATE PROCEDURE ptl.spGetArticleComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		ArticleComment.*
	FROM	
		[ptl].[ArticleComment] ArticleComment
	WHERE 
		ArticleComment.ID = @ID
END