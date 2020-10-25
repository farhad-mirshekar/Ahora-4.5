USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetLanguage'))
	DROP PROCEDURE pbl.spGetLanguage
GO

CREATE PROCEDURE pbl.spGetLanguage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		lng.*
	FROM pbl.[Language] lng
	WHERE (ID = @ID)

	RETURN @@ROWCOUNT
END