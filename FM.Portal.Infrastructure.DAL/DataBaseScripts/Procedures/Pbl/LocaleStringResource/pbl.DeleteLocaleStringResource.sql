USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteLocaleStringResource'))
	DROP PROCEDURE pbl.spDeleteLocaleStringResource
GO

CREATE PROCEDURE pbl.spDeleteLocaleStringResource
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	BEGIN TRAN
		DELETE FROM pbl.LocaleStringResource WHERE ID = @ID
	COMMIT
	RETURN @@ROWCOUNT
END