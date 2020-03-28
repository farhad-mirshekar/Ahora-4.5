USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetCategory'))
	DROP PROCEDURE ptl.spGetCategory
GO

CREATE PROCEDURE ptl.spGetCategory
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.*
	FROM	
		[ptl].[Category] cat
	WHERE 
		cat.ID = @ID
END