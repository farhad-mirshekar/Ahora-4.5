USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyMenu'))
	DROP PROCEDURE pbl.spModifyMenu
GO

CREATE PROCEDURE pbl.spModifyMenu
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(256),
	@LanguageID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 -- insert
		BEGIN
			INSERT INTO pbl.Menu
				(ID,[Name],[LanguageID],[UserID],[CreationDate])
			VALUES
				(@ID, @Name , @LanguageID , @UserID , GETDATE())
		END
	ELSE
		BEGIN
			UPDATE pbl.Menu
				SET 
					[Name] = @Name,
					[LanguageID] = @LanguageID
				WHERE ID = @ID
			END
	RETURN @@ROWCOUNT
END