USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsMenuItem'))
	DROP PROCEDURE pbl.spGetsMenuItem
GO

CREATE PROCEDURE pbl.spGetsMenuItem
@MenuID UNIQUEIDENTIFIER,
@ParentNode HIERARCHYID,
@LanguageID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		MenuItem.ID,
		MenuItem.[MenuID],
		MenuItem.[Node].ToString() AS [Node],
		MenuItem.[Node].GetAncestor(1).ToString() AS ParentNode,
		MenuItem.[Name],
		MenuItem.[Enabled],
		MenuItem.[Url],
		MenuItem.IconText,
		MenuItem.[Priority],
		MenuItem.[Parameters],
		MenuItem.CreationDate,
		MenuItem.ForeignLink
	FROM 
		pbl.MenuItem MenuItem
	INNER JOIN
		pbl.Menu Menu ON MenuItem.MenuID = Menu.ID
	WHERE
		Menu.RemoverID IS NULL
	AND	(@MenuID IS NULL OR MenuItem.MenuID = @MenuID)
	AND (@ParentNode IS NULL OR MenuItem.[Node].GetAncestor(1) = @ParentNode)
	AND (@LanguageID IS NULL OR Menu.LanguageID = @LanguageID)

	ORDER BY 
		MenuItem.[Priority] 

	RETURN @@ROWCOUNT
END