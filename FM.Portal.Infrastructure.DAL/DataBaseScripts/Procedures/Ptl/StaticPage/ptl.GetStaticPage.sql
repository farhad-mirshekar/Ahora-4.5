USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetStaticPage'))
	DROP PROCEDURE ptl.spGetStaticPage
GO

CREATE PROCEDURE ptl.spGetStaticPage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		DISTINCT
		staticPage.*,
		pages.CreationDate
	FROM ptl.StaticPage staticPage
	INNER JOIN
		ptl.Pages pages ON staticPage.ID = pages.ID
	LEFT JOIN
		pbl.Attachment attachemnt ON staticPage.AttachmentID = attachemnt.ParentID
	WHERE
		pages.PageType = 2
	AND	staticPage.ID = @ID

	RETURN @@ROWCOUNT
END