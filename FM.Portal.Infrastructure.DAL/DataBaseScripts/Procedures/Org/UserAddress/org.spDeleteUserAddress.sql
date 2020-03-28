USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetUserAddress'))
	DROP PROCEDURE org.spGetUserAddress
GO

CREATE PROCEDURE org.spGetUserAddress
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		*
	FROM org.[UserAddress]
	WHERE ID = @ID 

	RETURN @@ROWCOUNT
END