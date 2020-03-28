USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDisLikeComment'))
	DROP PROCEDURE app.spDisLikeComment
GO

CREATE PROCEDURE app.spDisLikeComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Like int;
	SET @Like = (SELECT DisLikeCount FROM app.Comment WHERE ID = @ID)
	SET @Like = @Like + 1;

	UPDATE app.Comment SET DisLikeCount = @Like WHERE ID = @ID
	SELECT @Like
END