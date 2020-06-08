USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsPage'))
	DROP PROCEDURE pbl.spGetsPage
GO

CREATE PROCEDURE pbl.spGetsPage

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		@ParentNode HIERARCHYID 

	--SET @ParentNode = (SELECT [Node] FROM org.Command WHERE ID = @ParentID)

	SELECT pages.ID,
		pages.[Node].ToString() Node,
		pages.[Node].GetAncestor(1).ToString() ParentNode,
		pages.ControllerName,
		pages.ActionName,
		pages.Title,
		Pages.[Description],
		pages.[Enabled],
		pages.[Deleted],
		pages.RouteUrl,
		pages.CreationDate
	FROM pbl.Pages pages
	ORDER BY Title

	RETURN @@ROWCOUNT
END