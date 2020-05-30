USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spModifyPages'))
	DROP PROCEDURE ptl.spModifyPages
GO

CREATE PROCEDURE ptl.spModifyPages
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Name Nvarchar(1000),
	@UrlDesc Nvarchar(1000),
	@PageType Tinyint,
	@UserID Uniqueidentifier,
	@Enabled Tinyint
--WITH ENCRYPTION
AS
BEGIN
	BEGIN TRAN
	IF @IsNewRecord = 1 -- insert
			BEGIN
				DECLARE @TrackingCode Nvarchar(20)
				SET @TrackingCode = (SELECT STR(FLOOR(RAND(CHECKSUM(NEWID()))*(9999999999-1000000000+1)+1000000000)))

				INSERT INTO ptl.Pages
				(ID,[TrackingCode],[Name],[UrlDesc],[PageType],[UserID],[Enabled],[CreationDate])
				VALUES
				(@ID, @TrackingCode,@Name,@UrlDesc,@PageType,@UserID,@Enabled,GETDATE())

				IF @PageType = 2
				BEGIN
				INSERT INTO ptl.StaticPage
				(ID,[TrackingCode])
				VALUES
				(@ID, @TrackingCode)
				END
			END
			ELSE
			BEGIN
				UPDATE ptl.Pages
				SET 
					[Name] = @Name,
					[UrlDesc] = @UrlDesc,
					[PageType] = @PageType,
					[UserID] = @UserID,
					[Enabled] = @Enabled
				WHERE ID = @ID

			END
		COMMIT
	RETURN @@ROWCOUNT
END