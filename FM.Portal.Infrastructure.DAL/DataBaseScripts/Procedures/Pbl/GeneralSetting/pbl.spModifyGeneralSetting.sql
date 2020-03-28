USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyGeneralSetting'))
	DROP PROCEDURE pbl.spModifyGeneralSetting
GO

CREATE PROCEDURE pbl.spModifyGeneralSetting
	@Name Nvarchar(256),
	@Value Nvarchar(max)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRAN
			UPDATE
				pbl.GeneralSetting
			SET
				[Value] = @Value
			WHERE
				[Name] = @Name
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END