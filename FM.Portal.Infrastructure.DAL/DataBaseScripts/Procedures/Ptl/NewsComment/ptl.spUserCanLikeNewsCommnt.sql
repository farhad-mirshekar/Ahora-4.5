USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spUserCanLikeNewsComment'))
	DROP PROCEDURE ptl.spUserCanLikeNewsComment
GO

CREATE PROCEDURE ptl.spUserCanLikeNewsComment
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
			ptl.NewsComment_User_Mapping
		WHERE 
			CommentID = @CommentID 
		AND	 UserID = @UserID
	)

	RETURN @Count
END