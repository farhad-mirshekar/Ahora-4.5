USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetMenu'))
	DROP PROCEDURE pbl.spGetMenu
GO

CREATE PROCEDURE pbl.spGetMenu
	@ID UNIQUEIDENTIFIER,
	@ParentNode HIERARCHYID
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;


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
		menu.[Priority]
	FROM 
		pbl.Menu menu
	WHERE 
		(@ID IS NULL OR ID = @ID) AND
		(@ParentNode IS NULL OR menu.[Node] = @ParentNode)

	RETURN @@ROWCOUNT
END