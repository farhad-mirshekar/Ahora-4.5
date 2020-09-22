USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsPage'))
	DROP PROCEDURE ptl.spGetsPage
GO

CREATE PROCEDURE ptl.spGetsPage
@PageType TINYINT,
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
			pages.*
		FROM ptl.Pages pages
		WHERE
			(@PageType IS NULL OR pages.PageType = @PageType)
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM 
			MainSelect
	)
	SELECT *
	FROM MainSelect , TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
	RETURN @@ROWCOUNT
END