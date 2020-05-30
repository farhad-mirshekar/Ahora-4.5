USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spModifyBanner'))
	DROP PROCEDURE ptl.spModifyBanner
GO

CREATE PROCEDURE ptl.spModifyBanner
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Description NVARCHAR(1000),
	@Name NVARCHAR(1000),
	@BannerType TINYINT,
	@Enabled TINYINT,
	@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO ptl.Banner
		(ID,[Name],[Description],[BannerType],[Enabled],[UserID],CreationDate)
		VALUES
		(@ID,@Name,@Description,@BannerType,@Enabled,@UserID,GETDATE())
	END
	ELSE
	BEGIN
		UPDATE ptl.Banner
		SET
			[Name] = @Name,
			[Description] = @Description,
			[BannerType] = @BannerType,
			[Enabled] = @Enabled
		WHERE
			ID = @ID
	END
	RETURN @@ROWCOUNT
END