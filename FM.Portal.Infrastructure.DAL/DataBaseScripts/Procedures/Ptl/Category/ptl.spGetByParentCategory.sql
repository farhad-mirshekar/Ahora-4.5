USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetByParentCategory'))
	DROP PROCEDURE ptl.spGetByParentCategory
GO

CREATE PROCEDURE ptl.spGetByParentCategory
@ParentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.*
	FROM	
		[ptl].[Category] cat
	WHERE 
		cat.ParentID = @ParentID
END