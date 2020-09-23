USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsStaticPage'))
	DROP PROCEDURE ptl.spGetsStaticPage
GO

CREATE PROCEDURE [ptl].spGetsStaticPage
@Name NVARCHAR(100),
@TrackingCode NVARCHAR(100),
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN
	SET @PageSize = COALESCE(@PageSize , 5)
	SET @PageIndex = COALESCE(@PageIndex,1)

	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END
	;WITH MainSelect AS
	(
		SELECT 
			staticPage.*,
			pages.[Name],
			pages.TrackingCode,
			pages.UrlDesc,
			pages.UserID,
			pages.CreationDate,
			attachemnt.[FileName],
			attachemnt.PathType
		FROM ptl.StaticPage staticPage
		INNER JOIN
			ptl.Pages pages ON staticPage.ID = pages.ID
		LEFT JOIN
			pbl.Attachment attachemnt ON staticPage.ID = attachemnt.ParentID
		WHERE
			pages.PageType = 2
		AND (@Name IS NULL OR pages.[Name] LIKE CONCAT('%' , @Name , '%'))
		AND (@TrackingCode IS NULL OR pages.TrackingCode LIKE CONCAT('%', @TrackingCode , '%'))
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)
	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END