USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCommentsForAdmin'))
	DROP PROCEDURE app.spGetCommentsForAdmin
GO

CREATE PROCEDURE app.spGetCommentsForAdmin
@commentForType Tinyint
--WITH ENCRYPTION
AS
BEGIN

;WITH main AS(
	SELECT 
		comment.*,
		CONCAT(users.FirstName ,' ',users.LastName) AS NameFamily,
		COALESCE(product.[Name] , article.Title , news.Title , events.Title) AS ProductName
	FROM	
		[app].[Comment] comment
	INNER JOIN
		[org].[User] users ON comment.UserID = users.ID
	LEFT JOIN
		app.Product product ON comment.DocumentID = product.ID
	LEFT JOIN
		ptl.Article article ON comment.DocumentID = article.ID
	LEFT JOIN
		ptl.News news ON comment.DocumentID = news.ID
	LEFT JOIN
		ptl.[Events] events ON comment.DocumentID = events.ID
	WHERE
		comment.CommentForType = @commentForType
		)

	SELECT * FROM main ORDER BY CreationDate
END