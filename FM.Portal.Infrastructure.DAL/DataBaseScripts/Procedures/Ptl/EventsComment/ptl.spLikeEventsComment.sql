USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spLikeEventsComment'))
	DROP PROCEDURE ptl.spLikeEventsComment
GO

CREATE PROCEDURE ptl.spLikeEventsComment
@ID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Like INT;
	SET @Like = (SELECT LikeCount FROM ptl.EventsComment WHERE ID = @ID)
	SET @Like = @Like + 1;
	
	BEGIN TRAN

		INSERT INTO ptl.EventsComment_User_Mapping (CommentID , UserID) VALUES(@ID , @UserID) -- add mapping

		UPDATE 
			ptl.EventsComment
		SET 
			LikeCount = @Like 
		WHERE 
			ID = @ID
	COMMIT
	
	RETURN @@ROWCOUNT
END