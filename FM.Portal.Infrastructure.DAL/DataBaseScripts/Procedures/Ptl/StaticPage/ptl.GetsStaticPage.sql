USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsStaticPage'))
	DROP PROCEDURE ptl.spGetsStaticPage
GO

CREATE PROCEDURE [ptl].spGetsStaticPage
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		staticPage.*,
		pages.[Name],
		pages.TrackingCode,
		pages.UrlDesc,
		pages.UserID,
		pages.CreationDate,
		attachemnt.[FileName],
		attachemnt.PathType
	FROM ptl.StaticPage staticPage
	INNER JOIN
		ptl.Pages pages ON staticPage.ID = pages.ID
	LEFT JOIN
		pbl.Attachment attachemnt ON staticPage.ID = attachemnt.ParentID
	WHERE
		pages.PageType = 2
	ORDER BY [CreationDate]

	RETURN @@ROWCOUNT
END