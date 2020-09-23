USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetStaticPage'))
	DROP PROCEDURE ptl.spGetStaticPage
GO

CREATE PROCEDURE ptl.spGetStaticPage
	@ID UNIQUEIDENTIFIER,
	@TrackingCode NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		staticPage.*,
		pages.[Name],
		pages.UrlDesc,
		pages.UserID,
		pages.CreationDate,
		pages.[Enabled],
		attachemnt.[FileName],
		attachemnt.PathType
	FROM ptl.StaticPage staticPage
	INNER JOIN
		ptl.Pages pages ON staticPage.ID = pages.ID
	LEFT JOIN
		pbl.Attachment attachemnt ON staticPage.AttachmentID = attachemnt.ParentID
	WHERE (@ID IS NULL OR pages.ID = @ID)
	AND (@TrackingCode IS NULL OR staticPage.TrackingCode = @TrackingCode)

	RETURN @@ROWCOUNT
END