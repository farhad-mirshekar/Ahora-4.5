USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetLocaleStringResource'))
	DROP PROCEDURE pbl.spGetLocaleStringResource
GO

CREATE PROCEDURE pbl.spGetLocaleStringResource
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		lsr.*
	FROM pbl.[LocaleStringResource] lsr
	WHERE (ID = @ID)

	RETURN @@ROWCOUNT
END