USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDisLikeEventsComment'))
	DROP PROCEDURE ptl.spDisLikeEventsComment
GO

CREATE PROCEDURE ptl.spDisLikeEventsComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @DisLike INT;
	SET @DisLike = (SELECT DisLikeCount FROM ptl.EventsComment WHERE ID = @ID)
	SET @DisLike = @DisLike + 1;

	BEGIN TRAN
		INSERT INTO ptl.EventsComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			ptl.EventsComment
		SET 
			DisLikeCount = @DisLike 
		WHERE 
			ID = @ID
	COMMIT

	SELECT @DisLike
END