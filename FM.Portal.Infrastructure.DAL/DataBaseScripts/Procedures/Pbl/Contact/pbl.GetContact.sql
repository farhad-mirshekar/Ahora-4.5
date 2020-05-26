USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsGeneralSetting'))
	DROP PROCEDURE pbl.spGetsGeneralSetting
GO

CREATE PROCEDURE pbl.spGetsGeneralSetting

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		*
	FROM pbl.GeneralSetting

	RETURN @@ROWCOUNT
END