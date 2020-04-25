USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsActiveNotification'))
	DROP PROCEDURE pbl.spGetsActiveNotification
GO

CREATE PROCEDURE pbl.spGetsActiveNotification
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		*
	FROM 
		pbl.[Notification]
	WHERE
		[UserID] = @UserID AND
		[ReadDate] IS NULL

	RETURN @@ROWCOUNT
END