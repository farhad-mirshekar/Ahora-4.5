USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsGalleryByCount'))
	DROP PROCEDURE ptl.spGetsGalleryByCount
GO

CREATE PROCEDURE [ptl].spGetsGalleryByCount
@Count INT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		Gallery.*,
		attachment.[FileName],
		attachment.PathType
	FROM 
		ptl.Gallery Gallery
	LEFT JOIN 
		pbl.Attachment attachment ON Gallery.ID = attachment.ParentID
	WHERE 
		attachment.[Type] = 1
	RETURN @@ROWCOUNT
END