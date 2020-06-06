USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetMenu'))
	DROP PROCEDURE pbl.spGetMenu
GO

CREATE PROCEDURE pbl.spGetMenu
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ParentID UNIQUEIDENTIFIER,
	@ParentNode HIERARCHYID
	SET @ParentNode = (SELECT Top 1 [Node].GetAncestor(1).ToString() FROM pbl.Menu WHERE ID = @ID)
	SET @ParentID = (SELECT ID FROM pbl.Menu WHERE Node = @ParentNode)

	SELECT 
		menu.ID,
		menu.[Node].ToString() AS [Node],
		menu.[Node].GetAncestor(1).ToString() AS ParentNode,
		menu.[Name],
		menu.RemoverID,
		menu.Deleted,
		menu.[Enabled],
		menu.IconText,
		menu.[Url],
		menu.[Priority],
		menu.[Parameters],
		menu.CreationDate,
		@ParentID ParentID
	FROM 
		pbl.Menu menu
	WHERE 
		(ID = @ID) 

	RETURN @@ROWCOUNT
END