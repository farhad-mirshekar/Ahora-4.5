USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetArticle'))
	DROP PROCEDURE ptl.spGetArticle
GO

CREATE PROCEDURE ptl.spGetArticle
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		article.*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName,
		attachment.[FileName],
		attachment.PathType
	FROM	
		[ptl].[Article] article
	INNER JOIN
		[org].[User] creator ON article.UserID = creator.ID
	INNER JOIN
		ptl.Category Category ON article.CategoryID = Category.ID
	LEFT JOIN 
		pbl.Attachment attachment ON article.ID = attachment.ParentID
	WHERE 
		article.RemoverID IS NULL
	AND Category.RemoverID IS NULL
	AND article.ID = @ID
END