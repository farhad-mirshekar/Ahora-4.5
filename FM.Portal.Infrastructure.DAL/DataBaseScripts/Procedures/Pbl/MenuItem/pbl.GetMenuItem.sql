USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetMenuItem'))
	DROP PROCEDURE pbl.spGetMenuItem
GO

CREATE PROCEDURE pbl.spGetMenuItem
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @ParentID UNIQUEIDENTIFIER,
	@ParentNode HIERARCHYID
	SET @ParentNode = (SELECT Top 1 [Node].GetAncestor(1).ToString() FROM pbl.MenuItem WHERE ID = @ID)
	SET @ParentID = (SELECT ID FROM pbl.MenuItem WHERE Node = @ParentNode)

	SELECT 
		MenuItem.ID,
		MenuItem.[MenuID],
		MenuItem.[Node].ToString() AS [Node],
		MenuItem.[Node].GetAncestor(1).ToString() AS ParentNode,
		MenuItem.[Name],
		MenuItem.[Enabled],
		MenuItem.IconText,
		MenuItem.[Url],
		MenuItem.[Priority],
		MenuItem.[Parameters],
		MenuItem.CreationDate,
		@ParentID ParentID,
		MenuItem.ForeignLink
	FROM 
		pbl.MenuItem MenuItem
	WHERE 
		(ID = @ID) 

	RETURN @@ROWCOUNT
END