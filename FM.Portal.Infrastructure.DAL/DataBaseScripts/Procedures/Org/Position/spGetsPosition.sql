USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsPosition'))
	DROP PROCEDURE org.spGetsPosition
GO

CREATE PROCEDURE org.spGetsPosition
@DepartmentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		position.ID,
		position.ApplicationID,
		position.[Default],
		position.DepartmentID,
		position.[Enabled],
		position.RemoveDate,
		position.RemoverID,
		position.[Type],
		position.UserID,
		position.[Node].ToString() Node,
		position.[Node].GetAncestor(1).ToString() ParentNode,
		CONCAT(users.FirstName,' ',users.LastName) UserInfo,
		Department.Name DepartmentName
	FROM org.Position position
	INNER JOIN org.[User] users ON position.UserID = users.ID
	INNER JOIN org.[Department] Department ON position.DepartmentID = Department.ID
	WHERE
		(@DepartmentID IS NULL OR DepartmentID = @DepartmentID)
	AND	(@UserID IS NULL OR position.UserID = @UserID)

	RETURN @@ROWCOUNT
END