USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetDynamicPage'))
	DROP PROCEDURE ptl.spGetDynamicPage
GO

CREATE PROCEDURE ptl.spGetDynamicPage
	@ID UNIQUEIDENTIFIER,
	@TrackingCode NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		dpages.*,
		pages.Name AS PageName,
		Pages.TrackingCode AS TrackingCodeParent,
		attachemnt.[FileName],
		attachemnt.PathType
	FROM ptl.DynamicPage dpages
	INNER JOIN
		ptl.Pages pages ON dpages.PageID = pages.ID
	LEFT JOIN
		pbl.Attachment attachemnt ON dpages.ID = attachemnt.ParentID
	WHERE (@ID IS NULL OR dpages.ID = @ID)
	AND (@TrackingCode IS NULL OR dpages.TrackingCode = @TrackingCode)

	RETURN @@ROWCOUNT
END