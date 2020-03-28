USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetByParentCategory'))
	DROP PROCEDURE app.spGetByParentCategory
GO

CREATE PROCEDURE app.spGetByParentCategory
@ParentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.*
	FROM	
		[app].[Category] cat
	WHERE 
		cat.ParentID = @ParentID
END