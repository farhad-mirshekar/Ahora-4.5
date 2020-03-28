USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spDeleteUserAddress'))
	DROP PROCEDURE org.spDeleteUserAddress
GO

CREATE PROCEDURE org.spDeleteUserAddress
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM org.UserAddress
	WHERE
		[ID] = @ID

	RETURN @@ROWCOUNT
END