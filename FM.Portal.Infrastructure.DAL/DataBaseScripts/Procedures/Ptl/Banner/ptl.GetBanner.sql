USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetBanner'))
	DROP PROCEDURE ptl.spGetBanner
GO

CREATE PROCEDURE ptl.spGetBanner
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		banner.*,
		attachemnt.[FileName],
		attachemnt.PathType
	FROM ptl.Banner banner
	LEFT JOIN
		pbl.Attachment attachemnt ON banner.ID = attachemnt.ParentID
	WHERE (banner.ID = @ID)

	RETURN @@ROWCOUNT
END