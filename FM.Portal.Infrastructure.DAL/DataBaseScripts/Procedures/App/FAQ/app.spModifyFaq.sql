USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyFaq'))
	DROP PROCEDURE app.spModifyFaq
GO

CREATE PROCEDURE app.spModifyFaq
@ID UNIQUEIDENTIFIER,
@FAQGroupID UNIQUEIDENTIFIER,
@Question NVARCHAR(500),
@Answer NVARCHAR(500),
@CreatorID UNIQUEIDENTIFIER,
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[FAQ]
				(ID,CreatorID , Answer , Question ,FAQGroupID, CreationDate)
			VALUES
				(@ID, @CreatorID , @Answer , @Question ,@FAQGroupID, GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[FAQ]
			SET
				[CreatorID] = @CreatorID ,
				[Answer] = @Answer ,
				[Question] = @Question
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END