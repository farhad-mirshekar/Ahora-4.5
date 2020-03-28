USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsCategory'))
	DROP PROCEDURE ptl.spGetsCategory
GO

CREATE PROCEDURE ptl.spGetsCategory
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM ptl.Category
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END