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
		slider.*
	FROM	
		[ptl].[Slider] slider
	WHERE 
		(slider.ID = @ID) 
END