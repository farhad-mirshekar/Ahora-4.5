USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetSlider'))
	DROP PROCEDURE ptl.spGetSlider
GO

CREATE PROCEDURE ptl.spGetSlider
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		slider.*,
		attachment.[FileName],
		attachment.PathType
	FROM	
		[ptl].[Slider] slider
	LEFT JOIN
		[pbl].[Attachment] attachment ON slider.ID = attachment.ParentID
	WHERE 
		(slider.ID = @ID) 
END