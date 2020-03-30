USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsEvents'))
	DROP PROCEDURE ptl.spGetsEvents
GO

CREATE PROCEDURE ptl.spGetsEvents
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM ptl.Events
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END