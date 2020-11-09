USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyNewsComment'))
	DROP PROCEDURE ptl.spModifyNewsComment
GO

CREATE PROCEDURE ptl.spModifyNewsComment
@ID UNIQUEIDENTIFIER,
@Body nvarchar(max),
@CommentType TINYINT,
@ParentID uniqueidentifier,
@UserID uniqueidentifier,
@NewsID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[NewsComment]
				([ID],Body,CreationDate,DisLikeCount,NewsID,LikeCount,ParentID,UserID)
			VALUES
				(@ID,@Body,GETDATE(),0,@NewsID,0,@ParentID,@UserID)
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[NewsComment]
			SET
				[Body] = @Body ,
				[CommentType] = @CommentType
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END