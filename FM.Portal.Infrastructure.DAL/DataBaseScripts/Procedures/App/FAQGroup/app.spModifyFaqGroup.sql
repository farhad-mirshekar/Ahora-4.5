USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyFaqGroup'))
	DROP PROCEDURE app.spModifyFaqGroup
GO

CREATE PROCEDURE app.spModifyFaqGroup
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(50),
@CreatorID UNIQUEIDENTIFIER,
@ApplicationID UNIQUEIDENTIFIER,
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[FAQGroup]
				(ID,CreatorID , ApplicationID , Title , CreationDate)
			VALUES
				(@ID, @CreatorID , @ApplicationID , @Title , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[FAQGroup]
			SET
				[CreatorID] = @CreatorID ,
				[ApplicationID] = @ApplicationID ,
				[Title] = @Title
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END