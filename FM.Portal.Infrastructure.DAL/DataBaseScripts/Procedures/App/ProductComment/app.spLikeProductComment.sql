USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spLikeProductComment'))
	DROP PROCEDURE app.spLikeProductComment
GO

CREATE PROCEDURE app.spLikeProductComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Like INT;
	SET @Like = (SELECT LikeCount FROM app.ProductComment WHERE ID = @ID)
	SET @Like = @Like + 1;
	
	BEGIN TRAN

		INSERT INTO app.ProductComment_User_Mapping(CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			app.ProductComment
		SET 
			LikeCount = @Like 
		WHERE 
			ID = @ID
	COMMIT
	
	RETURN @@ROWCOUNT
END