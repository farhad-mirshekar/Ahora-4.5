﻿USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spDeleteGallery'))
	DROP PROCEDURE ptl.spDeleteGallery
GO

CREATE PROCEDURE ptl.spDeleteGallery
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM ptl.Gallery
	WHERE ID = @ID
	RETURN @@ROWCOUNT

END