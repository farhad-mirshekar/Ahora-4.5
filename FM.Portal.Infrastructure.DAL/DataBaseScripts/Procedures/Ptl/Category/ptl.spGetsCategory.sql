USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsCategory'))
	DROP PROCEDURE ptl.spGetsCategory
GO

CREATE PROCEDURE ptl.spGetsCategory
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.ID,
		cat.[Node].ToString() Node,
		cat.[Node].GetAncestor(1).ToString() ParentNode,
		cat.CreationDate,
		cat.IncludeInLeftMenu,
		cat.IncludeInTopMenu,
		cat.Title
	FROM ptl.Category cat
	ORDER BY [CreationDate]
END