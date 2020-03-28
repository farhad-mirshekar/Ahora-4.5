USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetComment'))
	DROP PROCEDURE app.spGetComment
GO

CREATE PROCEDURE app.spGetComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		comment.*
	FROM	
		[app].[Comment] comment
	WHERE 
		comment.ID = @ID
END