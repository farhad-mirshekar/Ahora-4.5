USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsPosition'))
	DROP PROCEDURE org.spGetsPosition
GO

CREATE PROCEDURE org.spGetsPosition
@DepartmentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		position.*,
		CONCAT(users.FirstName,' ',users.LastName) UserInfo
	FROM org.Position position
	INNER JOIN org.[User] users ON position.UserID = users.ID
	WHERE
		(@DepartmentID IS NULL OR DepartmentID = @DepartmentID)

	RETURN @@ROWCOUNT
END