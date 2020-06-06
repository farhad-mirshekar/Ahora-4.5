USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsGallery'))
	DROP PROCEDURE ptl.spGetsGallery
GO

CREATE PROCEDURE [ptl].spGetsGallery
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
	gallery.*
	FROM ptl.Gallery gallery
	ORDER BY 
		gallery.CreationDate Desc
	RETURN @@ROWCOUNT
END