USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsSliderByCount'))
	DROP PROCEDURE ptl.spGetsSliderByCount
GO

CREATE PROCEDURE ptl.spGetsSliderByCount
@count int
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		slider.*,
		attachment.[FileName],
		attachment.PathType
	FROM 
		ptl.Slider slider
	LEFT JOIN 
		pbl.Attachment attachment ON slider.ID = attachment.ParentID
	WHERE 
		slider.Deleted = 0 AND
		slider.[Enabled] = 1
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END