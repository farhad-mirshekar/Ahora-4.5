USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyComment'))
	DROP PROCEDURE app.spModifyComment
GO

CREATE PROCEDURE app.spModifyComment
@ID UNIQUEIDENTIFIER,
@Body nvarchar(max),
@CommentType TinyINT,
@ParentID uniqueidentifier,
@UserID uniqueidentifier,
@DocumentID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Comment]
				([ID],Body,CommentType,CreationDate,DisLikeCount,DocumentID,LikeCount,ParentID,RemoverID,UserID)
			VALUES
				(NEWID() , @Body,@CommentType,GETDATE(),0,@DocumentID,0,@ParentID,null,@UserID)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Comment]
			SET
				[Body] = @Body ,
				[CommentType] = @CommentType
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END