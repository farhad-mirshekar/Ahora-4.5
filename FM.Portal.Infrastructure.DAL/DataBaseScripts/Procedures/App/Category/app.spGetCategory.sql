USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCategory'))
	DROP PROCEDURE app.spGetCategory
GO

CREATE PROCEDURE app.spGetCategory
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @ParentID UNIQUEIDENTIFIER,
	@ParentNode HIERARCHYID
	SET @ParentNode = (SELECT Top 1 [Node].GetAncestor(1).ToString() FROM app.Category WHERE ID = @ID)
	SET @ParentID = (SELECT ID FROM app.Category WHERE Node = @ParentNode)
	SELECT 
		cat.ID,
		cat.[Node].ToString() Node,
		cat.[Node].GetAncestor(1).ToString() ParentNode,
		cat.CreationDate,
		cat.IncludeInLeftMenu,
		cat.IncludeInTopMenu,
		cat.HasDiscountsApplied,
		cat.Title,
		@ParentID AS ParentID,
		map.DiscountID
	FROM	
		[app].[Category] cat
	LEFT JOIN
		[app].[Category_Discount_Mapping] map ON cat.ID = map.CategoryID
	WHERE 
		cat.ID = @ID
END