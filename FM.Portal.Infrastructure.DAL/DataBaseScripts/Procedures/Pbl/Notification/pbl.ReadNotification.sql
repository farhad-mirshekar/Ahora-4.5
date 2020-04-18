USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spReadNotification'))
	DROP PROCEDURE pbl.spReadNotification
GO

CREATE PROCEDURE pbl.spReadNotification
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 
		[pbl].[Notification]
	SET
		[ReadDate] = GETDATE()
	WHERE
		[ID] = @ID


	RETURN @@ROWCOUNT
END