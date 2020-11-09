USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDisLikeProductComment'))
	DROP PROCEDURE app.spDisLikeProductComment
GO

CREATE PROCEDURE app.spDisLikeProductComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @DisLike INT;
	SET @DisLike = (SELECT DisLikeCount FROM app.ProductComment WHERE ID = @ID)
	SET @DisLike = @DisLike + 1;

	BEGIN TRAN
		INSERT INTO app.ProductComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			app.ProductComment
		SET 
			DisLikeCount = @DisLike 
		WHERE 
			ID = @ID
	COMMIT

	SELECT @DisLike
END