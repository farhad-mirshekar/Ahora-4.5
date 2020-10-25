USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteLanguage'))
	DROP PROCEDURE pbl.spDeleteLanguage
GO

CREATE PROCEDURE pbl.spDeleteLanguage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	BEGIN TRAN
		DELETE FROM pbl.LocaleStringResource WHERE LanguageID = @ID
		DELETE FROM pbl.[Language] WHERE ID = @ID
	COMMIT
	RETURN @@ROWCOUNT
END