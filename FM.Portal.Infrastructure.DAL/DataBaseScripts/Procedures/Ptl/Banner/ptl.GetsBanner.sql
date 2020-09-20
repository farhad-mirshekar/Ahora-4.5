USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsBanner'))
	DROP PROCEDURE ptl.spGetsBanner
GO

CREATE PROCEDURE [ptl].spGetsBanner
@BannerType TINYINT,
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
			banner.*
		FROM 
			ptl.Banner banner
		WHERE
			(@BannerType IS NULL OR banner.BannerType = @BannerType)
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM
			MainSelect
	)
	SELECT *
	FROM MainSelect , TempCount
	ORDER BY 
		CreationDate Desc
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END