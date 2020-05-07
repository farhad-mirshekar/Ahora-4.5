USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spPositionDefault'))
	DROP PROCEDURE org.spPositionDefault
GO
CREATE PROCEDURE org.spPositionDefault
@UserID uniqueidentifier

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		users.Username,
		users.ID,
		positions.ID AS PositionID,
		positions.DepartmentID,
		positions.ApplicationID
	FROM 
		[org].[User] users
	INNER JOIN
		[org].[Position] positions ON users.ID = positions.UserID
		INNER JOIN
		[org].[Department] department ON positions.DepartmentID = department.ID
	WHERE
		(@UserID IS NULL OR users.[ID] = @UserID) AND
		users.[Enabled] = 1
END