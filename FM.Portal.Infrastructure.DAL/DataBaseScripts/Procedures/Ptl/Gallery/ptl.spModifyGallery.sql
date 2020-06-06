USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spModifyGallery'))
	DROP PROCEDURE ptl.spModifyGallery
GO

CREATE PROCEDURE ptl.spModifyGallery
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Description NVARCHAR(MAX),
	@Name NVARCHAR(1000),
	@VisitedCount INT,
	@Enabled TINYINT,
	@UserID UNIQUEIDENTIFIER,
	@MetaKeywords NVARCHAR(1000),
	@UrlDesc NVARCHAR(1000)
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		DECLARE @TrackingCode NVARCHAR(20)
		SET @TrackingCode = (select STR(FLOOR(RAND(CHECKSUM(NEWID()))*(9999999999-1000000000+1)+1000000000)))
		INSERT INTO ptl.Gallery
		(ID,[Name],[Description],[VisitedCount],[Enabled],[UserID],[TrackingCode],CreationDate,[MetaKeywords],[UrlDesc])
		VALUES
		(@ID,@Name,@Description,@VisitedCount,@Enabled,@UserID,@TrackingCode,GETDATE(),@MetaKeywords ,@UrlDesc)
	END
	ELSE
	BEGIN
		UPDATE ptl.Gallery
		SET
			[Name] = @Name,
			[Description] = @Description,
			[Enabled] = @Enabled,
			[UrlDesc] = @UrlDesc,
			[MetaKeywords] = @MetaKeywords
		WHERE
			ID = @ID
	END
	RETURN @@ROWCOUNT
END