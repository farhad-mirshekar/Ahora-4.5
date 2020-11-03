USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteMenuItem'))
	DROP PROCEDURE pbl.spDeleteMenuItem
GO

CREATE PROCEDURE pbl.spDeleteMenuItem
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Node HIERARCHYID
	SET @Node = (SELECT [Node] FROM pbl.MenuItem WHERE ID = @ID)

	DELETE pbl.MenuItem
	WHERE [Node].IsDescendantOf(@Node) = 1

	RETURN @@ROWCOUNT
END