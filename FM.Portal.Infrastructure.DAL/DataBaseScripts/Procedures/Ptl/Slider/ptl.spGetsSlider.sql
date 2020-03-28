USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsSlider'))
	DROP PROCEDURE ptl.spGetsSlider
GO

CREATE PROCEDURE ptl.spGetsSlider
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM ptl.Slider
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END