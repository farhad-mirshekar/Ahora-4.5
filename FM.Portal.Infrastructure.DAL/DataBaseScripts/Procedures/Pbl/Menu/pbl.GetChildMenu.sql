USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetChildMenu'))
	DROP PROCEDURE pbl.spGetChildMenu
GO

CREATE PROCEDURE pbl.spGetChildMenu
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
		menu.[Url],
		menu.IconText,
		menu.[Priority],
		menu.[Parameters]
	FROM 
		pbl.Menu menu
	WHERE 
		menu.[Enabled] = 1 AND
		(@ParentNode IS NULL OR menu.[Node].GetAncestor(1) = @ParentNode)
	ORDER BY
		menu.[Priority]

	RETURN @@ROWCOUNT
END