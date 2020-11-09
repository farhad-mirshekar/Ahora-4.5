USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyEventsComment'))
	DROP PROCEDURE ptl.spModifyEventsComment
GO

CREATE PROCEDURE ptl.spModifyEventsComment
@ID UNIQUEIDENTIFIER,
@Body nvarchar(max),
@CommentType TINYINT,
@ParentID uniqueidentifier,
@UserID uniqueidentifier,
@EventsID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[EventsComment]
				([ID],Body,CreationDate,DisLikeCount,EventsID,LikeCount,ParentID,UserID)
			VALUES
				(@ID,@Body,GETDATE(),0,@EventsID,0,@ParentID,@UserID)
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[EventsComment]
			SET
				[Body] = @Body ,
				[CommentType] = @CommentType
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END