USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetGallery'))
	DROP PROCEDURE ptl.spGetGallery
GO

CREATE PROCEDURE ptl.spGetGallery
	@ID UNIQUEIDENTIFIER,
	@TrackingCode NVARCHAR(20)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		gallery.*,
		attachemnt.[FileName],
		attachemnt.PathType
	FROM ptl.Gallery gallery
	LEFT JOIN
		pbl.Attachment attachemnt ON gallery.ID = attachemnt.ParentID
	WHERE (@ID IS NULL OR gallery.ID = @ID)
	AND (@TrackingCode IS NULL OR gallery.TrackingCode = @TrackingCode)

	RETURN @@ROWCOUNT
END