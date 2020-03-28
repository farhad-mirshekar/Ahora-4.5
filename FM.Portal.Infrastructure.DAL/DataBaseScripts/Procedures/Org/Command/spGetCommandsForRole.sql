USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetCommandsForRole'))
	DROP PROCEDURE org.spGetCommandsForRole
GO

CREATE PROCEDURE org.spGetCommandsForRole
	--@ParentID UNIQUEIDENTIFIER,
	@ApplicationID UNIQUEIDENTIFIER,
	@RoleID UNIQUEIDENTIFIER

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		@ParentNode HIERARCHYID 

	--SET @ParentNode = (SELECT [Node] FROM org.Command WHERE ID = @ParentID)

	SELECT Command.ID,
		Command.ApplicationID,
		Command.[Node].ToString() Node,
		Command.[Node].GetAncestor(1).ToString() ParentNode,
		Command.[Name],
		Command.FullName,
		Command.Title,
		Command.[Type]
	FROM org.Command
		INNER JOIN org.RolePermission ON RolePermission.CommandID = Command.ID AND RolePermission.RoleID = @RoleID
	WHERE ApplicationID = @ApplicationID AND Command.Node != 0x58
	ORDER BY Title

	RETURN @@ROWCOUNT
END