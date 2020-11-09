USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductComment'))
	DROP PROCEDURE app.spModifyProductComment
GO

CREATE PROCEDURE app.spModifyProductComment
@ID UNIQUEIDENTIFIER,
@Body nvarchar(max),
@CommentType TINYINT,
@ParentID uniqueidentifier,
@UserID uniqueidentifier,
@ProductID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ProductComment]
				([ID],Body,CreationDate,DisLikeCount,ProductID,LikeCount,ParentID,UserID)
			VALUES
				(@ID,@Body,GETDATE(),0,@ProductID,0,@ParentID,@UserID)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ProductComment]
			SET
				[Body] = @Body ,
				[CommentType] = @CommentType
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END