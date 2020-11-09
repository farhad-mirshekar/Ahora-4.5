USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spUserCanLikeEventsComment'))
	DROP PROCEDURE ptl.spUserCanLikeEventsComment
GO

CREATE PROCEDURE ptl.spUserCanLikeEventsComment
@CommentID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @Count INT;
	SET @Count = 
	(
		SELECT 
			COUNT(*)
		FROM 
			ptl.EventsComment_User_Mapping
		WHERE 
			CommentID = @CommentID 
		AND	 UserID = @UserID
	)

	RETURN @Count
END