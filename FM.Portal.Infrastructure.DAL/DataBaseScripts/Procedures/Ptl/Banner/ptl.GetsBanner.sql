USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsBanner'))
	DROP PROCEDURE ptl.spGetsBanner
GO

CREATE PROCEDURE [ptl].spGetsBanner
@BannerType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
	banner.*
	FROM ptl.Banner banner
	WHERE
		(@BannerType IS NULL OR banner.BannerType = @BannerType)
	ORDER BY 
		banner.CreationDate Desc
	RETURN @@ROWCOUNT
END