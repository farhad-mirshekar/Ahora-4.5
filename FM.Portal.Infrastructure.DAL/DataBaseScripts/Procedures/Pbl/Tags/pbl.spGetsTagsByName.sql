USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsTagsByName'))
	DROP PROCEDURE pbl.spGetsTagsByName
GO

CREATE PROCEDURE pbl.spGetsTagsByName
	@Name NVARCHAR(MAX)
--WITH ENCRYPTION
AS
BEGIN

	DECLARE @NewName NVARCHAR(MAX) = LTRIM(RTRIM(@Name)) 
	
	SELECT 
		tag.[Name],
		COALESCE(news.ID,events.ID,article.ID , product.ID) AS DocumentID,
		CAST((
				CASE 
					WHEN news.ID IS NOT NULL THEN 3
					WHEN events.ID IS NOT NULL THEN 8
					WHEN article.ID IS NOT NULL THEN 4
					WHEN product.ID IS NOT NULL THEN 6
					ELSE 0 END
			)AS TINYINT) AS DocumentType
	FROM 
		pbl.Tags tag
	INNER JOIN 
		pbl.Tags_Mapping map ON tag.ID = map.TagID
	LEFT JOIN 
		ptl.[Events] events ON map.DocumentID = events.ID
	LEFT JOIN 
		ptl.News news ON map.DocumentID = news.ID
	LEFT JOIN
		 ptl.Article article ON map.DocumentID = article.ID
	LEFT JOIN
		 app.Product product ON map.DocumentID = product.ID
	WHERE
		tag.Name = @NewName
END