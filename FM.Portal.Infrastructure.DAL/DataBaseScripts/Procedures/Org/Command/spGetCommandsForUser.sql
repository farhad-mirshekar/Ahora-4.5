USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetCommandsForUser'))
	DROP PROCEDURE org.spGetCommandsForUser
GO

CREATE PROCEDURE org.spGetCommandsForUser
	@PositionID UNIQUEIDENTIFIER

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		command.ID,
		command.[Name],
		command.Title,
		command.[Type],
		command.FullName
	FROM org.PositionRole positionrole
		INNER JOIN org.[Role] roles ON roles.ID = positionrole.RoleID
		INNER JOIN org.RolePermission permission ON positionrole.RoleID = permission.RoleID
		INNER JOIN org.Command command ON command.ID = permission.CommandID
	WHERE 
		positionrole.PositionID = @PositionID
END