USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetNewsComment'))
	DROP PROCEDURE ptl.spGetNewsComment
GO

CREATE PROCEDURE ptl.spGetNewsComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		NewsComment.*,
		News.Title AS NewsTitle
	FROM	
		[ptl].[NewsComment] NewsComment
	INNER JOIN
		ptl.News News ON NewsComment.NewsID = News.ID
	WHERE 
		NewsComment.ID = @ID
	AND NewsComment.RemoverID IS NULL
	AND News.RemoverID IS NULL
END