USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetActivityLog'))
	DROP PROCEDURE pbl.spGetActivityLog
GO

CREATE PROCEDURE pbl.spGetActivityLog
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		*
	FROM pbl.ActivityLog
	WHERE ID = @ID
END