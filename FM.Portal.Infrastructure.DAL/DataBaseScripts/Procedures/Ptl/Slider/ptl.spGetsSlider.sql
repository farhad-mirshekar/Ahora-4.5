USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsSlider'))
	DROP PROCEDURE ptl.spGetsSlider
GO

CREATE PROCEDURE ptl.spGetsSlider
@Title NVARCHAR(100),
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
	;WITH Attachment AS
	(
		SELECT
			Attachment.ParentID,
			Attachment.[FileName],
			Attachment.PathType
		FROM pbl.Attachment Attachment 
		INNER JOIN ptl.Slider Slider ON Attachment.ParentID = Slider.ID
	),  MainSelect AS
	(
		SELECT 
			Slider.*,
			Attachment.[FileName],
			Attachment.PathType
		FROM 
			ptl.Slider Slider
		LEFT JOIN 
			Attachment Attachment ON Slider.ID = Attachment.ParentID
		WHERE 
			Slider.Deleted = 0
		AND Slider.[Enabled] = 1
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