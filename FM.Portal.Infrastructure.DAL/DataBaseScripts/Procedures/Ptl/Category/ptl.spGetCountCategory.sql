USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetCountCategory'))
	DROP PROCEDURE ptl.spGetCountCategory
GO

CREATE PROCEDURE ptl.spGetCountCategory
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.Title,
		COUNT(*) AS Count
	FROM	
		[ptl].[Category] cat
	INNER JOIN
		[ptl].[Article] article ON cat.ID  = article.CategoryID
	WHERE
		article.RemoverID IS NULL
	GROUP BY 
		cat.[Title]
END