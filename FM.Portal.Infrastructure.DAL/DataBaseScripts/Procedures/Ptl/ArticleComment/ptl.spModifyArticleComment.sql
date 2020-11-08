USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyArticleComment'))
	DROP PROCEDURE ptl.spModifyArticleComment
GO

CREATE PROCEDURE ptl.spModifyArticleComment
@ID UNIQUEIDENTIFIER,
@Body nvarchar(max),
@CommentType TINYINT,
@ParentID uniqueidentifier,
@UserID uniqueidentifier,
@ArticleID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[ArticleComment]
				([ID],Body,CreationDate,DisLikeCount,ArticleID,LikeCount,ParentID,UserID)
			VALUES
				(@ID,@Body,GETDATE(),0,@ArticleID,0,@ParentID,@UserID)
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[ArticleComment]
			SET
				[Body] = @Body ,
				[CommentType] = @CommentType
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END