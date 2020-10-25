USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyLocaleStringResource'))
	DROP PROCEDURE pbl.spModifyLocaleStringResource
GO

CREATE PROCEDURE pbl.spModifyLocaleStringResource
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@ResourceName NVARCHAR(3000),
	@ResourceValue NVARCHAR(3000),
	@LanguageID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.[LocaleStringResource] (ID,UserID,CreationDate,[LanguageID] , [ResourceName],[ResourceValue])
		VALUES
		(@ID,@UserID,GETDATE(),@LanguageID , @ResourceName,@ResourceValue)
	END
	ELSE
	BEGIN
		UPDATE pbl.[LocaleStringResource]
		SET
			[UserID] = @UserID,
			[LanguageID] = @LanguageID,
			[ResourceName] = @ResourceName,
			[ResourceValue] = @ResourceValue
		WHERE
			ID = @ID
	END
	RETURN @@ROWCOUNT
END