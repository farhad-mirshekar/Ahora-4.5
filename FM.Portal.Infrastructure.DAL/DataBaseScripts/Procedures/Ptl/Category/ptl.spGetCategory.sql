USE [Ahora]
GO

/****** Object:  StoredProcedure [ptl].[spGetCategory]    Script Date: 5/24/2020 1:48:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [ptl].[spGetCategory]
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @ParentID UNIQUEIDENTIFIER,
	@ParentNode HIERARCHYID
	SET @ParentNode = (SELECT Top 1 [Node].GetAncestor(1).ToString() FROM ptl.Category WHERE ID = @ID)
	SET @ParentID = (SELECT ID FROM ptl.Category WHERE Node = @ParentNode)
	SELECT 
		cat.ID,
		cat.[Node].ToString() Node,
		cat.[Node].GetAncestor(1).ToString() ParentNode,
		cat.CreationDate,
		cat.Title,
		cat.RemoverID,
		cat.RemoverDate,
		@ParentID AS ParentID
	FROM	
		[ptl].[Category] cat
	WHERE 
		cat.ID = @ID
END
GO


