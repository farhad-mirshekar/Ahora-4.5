USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteMenu'))
	DROP PROCEDURE pbl.spDeleteMenu
GO

CREATE PROCEDURE pbl.spDeleteMenu
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	DECLARE @Node HIERARCHYID
	SET @Node = (SELECT [Node] FROM pbl.Menu WHERE ID = @ID )
	DELETE FROM pbl.Menu
	WHERE [Node].IsDescendantOf(@Node) = 1
	RETURN @@ROWCOUNT

END