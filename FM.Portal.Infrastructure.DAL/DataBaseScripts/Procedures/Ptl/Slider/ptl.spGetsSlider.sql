USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsSlider'))
	DROP PROCEDURE ptl.spGetsSlider
GO

CREATE PROCEDURE ptl.spGetsSlider
@Title NVARCHAR(100),
@Enabled TINYINT,
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
			Slider.*
		FROM 
			ptl.Slider Slider
		WHERE 
		  (@Enabled < 1 OR Slider.[Enabled] = @Enabled)
		AND (@Title IS NULL OR Slider.Title LIKE CONCAT('%' , @Title , '%'))
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)

	SELECT * 
	FROM MainSelect , TempCount
	ORDER BY [CreationDate] DESC
	OFFSET((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END