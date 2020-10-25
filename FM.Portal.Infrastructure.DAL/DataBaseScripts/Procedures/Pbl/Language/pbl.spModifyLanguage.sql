USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyLanguage'))
	DROP PROCEDURE pbl.spModifyLanguage
GO

CREATE PROCEDURE pbl.spModifyLanguage
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@Name NVARCHAR(1000),
	@LanguageCultureType TINYINT,
	@UniqueSeoCode NVARCHAR(100),
	@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.[Language] (ID,UserID,[Enabled],CreationDate,[Name],[LanguageCultureType],[UniqueSeoCode])
		VALUES
		(@ID,@UserID,@Enabled,GETDATE(),@Name,@LanguageCultureType,@UniqueSeoCode)
	END
	ELSE
	BEGIN
		UPDATE pbl.[Language]
		SET
			[Enabled] = @Enabled,
			[Name] = @Name,
			[LanguageCultureType] = @LanguageCultureType,
			[UniqueSeoCode] = @UniqueSeoCode
		WHERE
			ID = @ID
	END
	RETURN @@ROWCOUNT
END