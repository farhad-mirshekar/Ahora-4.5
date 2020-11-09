USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetNewsComment'))
	DROP PROCEDURE ptl.spGetNewsComment
GO

CREATE PROCEDURE ptl.spGetNewsComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		NewsComment.*
	FROM	
		[ptl].[NewsComment] NewsComment
	WHERE 
		NewsComment.ID = @ID
END