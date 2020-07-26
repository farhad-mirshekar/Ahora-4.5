USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsEmailLogs'))
	DROP PROCEDURE pbl.spGetsEmailLogs
GO

CREATE PROCEDURE pbl.spGetsEmailLogs

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		*
	FROM pbl.EmailLogs

	RETURN @@ROWCOUNT
END