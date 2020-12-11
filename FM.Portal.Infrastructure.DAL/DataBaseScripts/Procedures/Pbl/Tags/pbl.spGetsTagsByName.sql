USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsTagsByName'))
	DROP PROCEDURE pbl.spGetsTagsByName
GO

CREATE PROCEDURE pbl.spGetsTagsByName
	@Name NVARCHAR(MAX),
	@PageIndex INT,
	@PageSize INT
--WITH ENCRYPTION
AS
BEGIN
	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END

	;WITH Article AS
	(
		SELECT
			Article.[Description],
			Article.[Title],
			UrlRecord.EntityID,
			UrlRecord.EntityName,
			UrlRecord.UrlDesc
		FROM 
			ptl.Article Article
		INNER JOIN 
			pbl.UrlRecord UrlRecord ON Article.ID = UrlRecord.EntityID
		WHERE 
			Article.RemoverID IS NULL
	),
	News AS
	(
		SELECT
			News.[Description],
			News.[Title],
			UrlRecord.EntityID,
			UrlRecord.EntityName,
			UrlRecord.UrlDesc
		FROM 
			ptl.News News
		INNER JOIN 
			pbl.UrlRecord UrlRecord ON News.ID = UrlRecord.EntityID
		WHERE 
			News.RemoverID IS NULL
	),
	[Events] AS
	(
		SELECT
			[Events].[Description],
			[Events].[Title],
			UrlRecord.EntityID,
			UrlRecord.EntityName,
			UrlRecord.UrlDesc
		FROM 
			ptl.[Events] [Events]
		INNER JOIN 
			pbl.UrlRecord UrlRecord ON [Events].ID = UrlRecord.EntityID
		WHERE 
			[Events].RemoverID IS NULL
	),
	Product AS
	(
		SELECT
			Product.ShortDescription,
			Product.[Name],
			UrlRecord.EntityID,
			UrlRecord.EntityName,
			UrlRecord.UrlDesc
		FROM 
			app.Product Product
		INNER JOIN 
			pbl.UrlRecord UrlRecord ON Product.ID = UrlRecord.EntityID
		WHERE 
			Product.RemoverID IS NULL
	),
	DynamicPage AS
	(
		SELECT
			DynamicPage.[Description],
			DynamicPage.[Name],
			UrlRecord.EntityID,
			UrlRecord.EntityName,
			UrlRecord.UrlDesc,
			Pages.Name AS PageName
		FROM 
			ptl.DynamicPage DynamicPage
		INNER JOIN 
			pbl.UrlRecord UrlRecord ON DynamicPage.ID = UrlRecord.EntityID
		INNER JOIN
			ptl.Pages Pages ON DynamicPage.PageID = Pages.ID
	)
	,MainSelect AS
	(
		SELECT 
			Tags.[Name],
			COALESCE(News.Title,[Events].Title,Article.Title , Product.[Name],DynamicPage.[Name]) AS DocumentTitle,
			COALESCE(News.[Description],[Events].[Description],Article.[Description] , Product.ShortDescription,DynamicPage.[Description]) AS DocumentDescription,
			COALESCE(News.EntityID,[Events].EntityID,Article.EntityID , Product.EntityID,DynamicPage.EntityID) AS DocumentID,
			COALESCE(News.UrlDesc,[Events].UrlDesc,Article.UrlDesc , Product.UrlDesc,DynamicPage.UrlDesc) AS DocumentUrlDesc,
			COALESCE(News.EntityName,[Events].EntityName,Article.EntityName , Product.EntityName,DynamicPage.EntityName) AS EntityName,
			ISNULL(DynamicPage.PageName , '') AS PageName
		FROM 
			pbl.Tags Tags 
		INNER JOIN	
			pbl.Tags_Mapping Tags_Mapping ON Tags.ID = Tags_Mapping.TagID
		LEFT JOIN
			Article Article ON Tags_Mapping.DocumentID = Article.EntityID
		LEFT JOIN
			News News ON Tags_Mapping.DocumentID = News.EntityID
		LEFT JOIN
			[Events] [Events] ON Tags_Mapping.DocumentID = [Events].EntityID
		LEFT JOIN
			Product Product ON Tags_Mapping.DocumentID = Product.EntityID
		LEFT JOIN
			DynamicPage DynamicPage ON Tags_Mapping.DocumentID = DynamicPage.EntityID 
		WHERE
			Tags.[Name] LIKE CONCAT('%',@Name,'%') 
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)

	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [DocumentTitle] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END