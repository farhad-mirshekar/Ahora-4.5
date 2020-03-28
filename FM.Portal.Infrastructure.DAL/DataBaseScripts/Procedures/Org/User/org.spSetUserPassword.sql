USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spSetUserPassword'))
	DROP PROCEDURE org.spSetUserPassword
GO

CREATE PROCEDURE org.spSetUserPassword
	@ID UNIQUEIDENTIFIER,
	@Password VARCHAR(1000),
	@PasswordExpireDate SMALLDateTIME
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRAN

			UPDATE org.[User]
			SET [Password] = @Password,
				PasswordExpireDate = COALESCE(@PasswordExpireDate, DATEADD(month, 6, GETDATE()))
			WHERE ID = @ID
			SELECT 1
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
END