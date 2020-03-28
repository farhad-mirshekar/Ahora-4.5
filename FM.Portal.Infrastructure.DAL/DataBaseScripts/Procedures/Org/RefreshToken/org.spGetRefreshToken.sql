USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetRefreshToken'))
	DROP PROCEDURE org.spGetRefreshToken
GO

CREATE PROCEDURE org.spGetRefreshToken
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ID
		 , UserID
		 , IssuedDate
		 , [ExpireDate]
		 , ProtectedTicket
	FROM org.RefreshToken
	WHERE ID = @ID 

	RETURN @@ROWCOUNT
END