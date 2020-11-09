USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spUserCanLikeProductComment'))
	DROP PROCEDURE app.spUserCanLikeProductComment
GO

CREATE PROCEDURE app.spUserCanLikeProductComment
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
			app.ProductComment_User_Mapping
		WHERE 
			CommentID = @CommentID 
		AND	 UserID = @UserID
	)

	RETURN @Count
END