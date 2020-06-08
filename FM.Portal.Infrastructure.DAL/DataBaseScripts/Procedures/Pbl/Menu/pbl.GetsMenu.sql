USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsMenu'))
	DROP PROCEDURE pbl.spGetsMenu
GO

CREATE PROCEDURE pbl.spGetsMenu

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
		menu.RemoverDate,
		menu.[Enabled],
		menu.[Url],
		menu.IconText,
		menu.[Priority],
		menu.[Parameters],
		menu.CreationDate,
		menu.ForeignLink
	FROM pbl.Menu menu
	WHERE menu.RemoverID IS NULL
	ORDER BY menu.[Priority] 

	RETURN @@ROWCOUNT
END