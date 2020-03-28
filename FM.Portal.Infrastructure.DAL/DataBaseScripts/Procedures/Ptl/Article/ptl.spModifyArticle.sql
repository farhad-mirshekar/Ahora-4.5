USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyArticle'))
	DROP PROCEDURE ptl.spModifyArticle
GO

CREATE PROCEDURE ptl.spModifyArticle
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(max),
@Body nvarchar(max),
@MetaKeywords nvarchar(400),
@Description nvarchar(max),
@CommentStatus Tinyint,
@UrlDesc nvarchar(max),
@IsShow bit,
@CategoryID uniqueidentifier,
@UserID uniqueidentifier,
@TrackingCode nvarchar(100),
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN

			DECLARE @total INT ;
			SET @total = (SELECT COUNT(*) FROM [ptl].[Article])
			SET @total = @total + 1
			SET @TrackingCode = CONCAT(@TrackingCode ,'',@total)

			INSERT INTO [ptl].[Article]
				([ID],Body,CommentStatus,CreationDate,[Description],DisLikeCount,IsShow,LikeCount,MetaKeywords,ModifiedDate,RemoverID,Title,UrlDesc,UserID,VisitedCount , TrackingCode,CategoryID)
			VALUES
				(@ID , @Body  , @CommentStatus , GETDATE() , @Description , 0,@IsShow , 0 , @MetaKeywords , GETDATE() , null , @Title ,@UrlDesc, @UserID , 0 , @TrackingCode,@CategoryID)
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[Article]
			SET
				[Title] = @Title ,
				[ModifiedDate] = GETDATE() ,
				[Body] = @Body ,
				[MetaKeywords] = @MetaKeywords,
				[Description] = @Description,
				[CommentStatus] = @CommentStatus,
				[UrlDesc] = @UrlDesc,
				[IsShow] = @IsShow,
				[CategoryID] = @CategoryID,
				[UserID] = @UserID
			WHERE
				[ID] = @ID
		END
			RETURN @@ROWCOUNT
END
