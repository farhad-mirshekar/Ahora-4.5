USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsDynamicPage'))
	DROP PROCEDURE ptl.spGetsDynamicPage
GO

CREATE PROCEDURE [ptl].[spGetsDynamicPage]
@PageID UNIQUEIDENTIFIER,
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
			dpages.*,
			pages.[Name] AS PageName,
			attachment.[FileName]
		FROM ptl.DynamicPage dpages
		INNER JOIN
			ptl.Pages pages ON dpages.PageID = pages.ID
		LEFT JOIN
			pbl.Attachment attachment ON dpages.ID = attachment.ParentID
		WHERE
			dpages.PageID = @PageID
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM
			MainSelect
	)
	SELECT *
	FROM MainSelect , TempCount
	ORDER BY [CreationDate]
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END