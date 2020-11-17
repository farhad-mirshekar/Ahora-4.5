USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyNews'))
	DROP PROCEDURE ptl.spModifyNews
GO

CREATE PROCEDURE ptl.spModifyNews
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(max),
@Body nvarchar(max),
@MetaKeywords nvarchar(400),
@Description nvarchar(max),
@CommentStatusType Tinyint,
@UrlDesc nvarchar(max),
@ViewStatusType tinyint,
@CategoryID uniqueidentifier,
@UserID uniqueidentifier,
@IsNewRecord BIT,
@ReadingTime NVARCHAR(200),
@LanguageID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN

	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[News]
				([ID],Body,CommentStatusType,CreationDate,[Description],DisLikeCount,ViewStatusType,LikeCount,MetaKeywords,ModifiedDate,RemoverID,Title,UrlDesc,UserID,VisitedCount ,CategoryID,ReadingTime,LanguageID)
			VALUES
				(@ID , @Body  , @CommentStatusType , GETDATE() , @Description , 0,@ViewStatusType , 0 , @MetaKeywords , GETDATE() , null , @Title ,@UrlDesc, @UserID , 0 ,@CategoryID,@ReadingTime,@LanguageID)
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[News]
			SET
				[Title] = @Title ,
				[ModifiedDate] = GETDATE() ,
				[Body] = @Body ,
				[MetaKeywords] = @MetaKeywords,
				[Description] = @Description,
				[CommentStatusType] = @CommentStatusType,
				[UrlDesc] = @UrlDesc,
				[ViewStatusType] = @ViewStatusType,
				[CategoryID] = @CategoryID,
				[UserID] = @UserID,
				[ReadingTime] = @ReadingTime,
				[LanguageID] = @LanguageID
			WHERE
				[ID] = @ID
		END
	RETURN @@ROWCOUNT
END
