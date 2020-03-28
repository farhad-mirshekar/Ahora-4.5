USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetCommands'))
	DROP PROCEDURE org.spGetCommands
GO

CREATE PROCEDURE org.spGetCommands

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
	ORDER BY Title

	RETURN @@ROWCOUNT
END