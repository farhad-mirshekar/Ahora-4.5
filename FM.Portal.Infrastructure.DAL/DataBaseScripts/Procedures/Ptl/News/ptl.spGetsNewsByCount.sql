USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsNewsByCount'))
	DROP PROCEDURE ptl.spGetsNewsByCount
GO

CREATE PROCEDURE ptl.spGetsNewsByCount
@count int
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		news.*,
		attachment.[FileName],
		attachment.PathType
	FROM 
		ptl.News news
	LEFT JOIN 
		pbl.Attachment attachment ON news.ID = attachment.ParentID
	WHERE 
		news.RemoverID IS NULL 
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END