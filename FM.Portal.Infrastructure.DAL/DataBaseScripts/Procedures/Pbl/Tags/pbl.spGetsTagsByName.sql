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
	;WITH Article AS
	(
		SELECT
			ID,
			Title,
			UrlDesc,
			TrackingCode
		FROM ptl.Article
		WHERE RemoverID IS NULL
	),
	News AS
	(
		SELECT
			ID,
			Title,
			UrlDesc,
			TrackingCode
		FROM ptl.News
		WHERE RemoverID IS NULL
	),
	Events AS
	(
		SELECT
			ID,
			Title,
			UrlDesc,
			TrackingCode
		FROM ptl.Events
		WHERE RemoverID IS NULL
	),
	Product AS
	(
		SELECT
			ID,
			Name,
			MetaTitle,
			TrackingCode
		FROM app.Product
		WHERE RemoverID IS NULL
	)
	
	SELECT 
		tag.[Name],
		COALESCE(news.ID,events.ID,article.ID , product.ID) AS DocumentID,
		COALESCE(news.TrackingCode,events.TrackingCode,article.TrackingCode , product.TrackingCode) AS TrackingCode,
		COALESCE(news.Title,events.Title,article.Title , product.[Name]) AS DocumentTitle,
		COALESCE(news.UrlDesc,events.UrlDesc,article.UrlDesc , product.MetaTitle) AS DocumentUrlDesc,
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
		Events events ON map.DocumentID = events.ID
	LEFT JOIN 
		News news ON map.DocumentID = news.ID
	LEFT JOIN
		 Article article ON map.DocumentID = article.ID
	LEFT JOIN
		 Product product ON map.DocumentID = product.ID
	WHERE
		tag.Name LIKE CONCAT('%', @NewName,'%')
END