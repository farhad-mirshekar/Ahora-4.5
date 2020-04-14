USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsUserAddress'))
	DROP PROCEDURE org.spGetsUserAddress
GO

CREATE PROCEDURE org.spGetsUserAddress
	@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN

	SET NOCOUNT ON;

	;WITH MainSelect AS (
	SELECT 
		useraddress.*,
		users.CellPhone
	FROM
		org.UserAddress useraddress
	INNER JOIN
		org.[User] users
	ON
		useraddress.UserID = users.ID
	WHERE
		users.ID = @UserID AND
		users.Enabled = 1
	)
	SELECT * FROM MainSelect		 
	ORDER BY [CreationDate] DESC

	RETURN @@ROWCOUNT
END