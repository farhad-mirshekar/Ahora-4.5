USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductComment'))
	DROP PROCEDURE app.spGetProductComment
GO

CREATE PROCEDURE app.spGetProductComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		ProductComment.*
	FROM	
		[app].[ProductComment] ProductComment
	WHERE 
		ProductComment.ID = @ID
END