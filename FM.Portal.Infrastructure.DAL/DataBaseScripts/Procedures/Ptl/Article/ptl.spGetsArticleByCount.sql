USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsArticleByCount'))
	DROP PROCEDURE ptl.spGetsArticleByCount
GO

CREATE PROCEDURE ptl.spGetsArticleByCount
@count int
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		article.* ,
		CONCAT(creator.FirstName , ' ', creator.LastName) AS CreatorName,
		attachment.[FileName],
		attachment.PathType
	FROM 
		ptl.Article article
	INNER JOIN	
		org.[User] creator ON article.UserID = creator.ID
	LEFT JOIN 
		pbl.Attachment attachment ON article.ID = attachment.ParentID
	WHERE
		article.RemoverID IS NULL
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END