USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsArticle'))
	DROP PROCEDURE ptl.spGetsArticle
GO

CREATE PROCEDURE ptl.spGetsArticle
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM ptl.Article
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END