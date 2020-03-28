USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spLikeComment'))
	DROP PROCEDURE app.spLikeComment
GO

CREATE PROCEDURE app.spLikeComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Like int;
	SET @Like = (SELECT LikeCount FROM app.Comment WHERE ID = @ID)
	SET @Like = @Like + 1;

	UPDATE app.Comment SET LikeCount = @Like WHERE ID = @ID
	SELECT @Like
END