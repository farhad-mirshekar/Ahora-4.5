USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetNotification'))
	DROP PROCEDURE pbl.spGetNotification
GO

CREATE PROCEDURE pbl.spGetNotification
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN

	SELECT 
		*
	FROM 
		pbl.[Notification]
	WHERE
		[ID] = @ID 
END