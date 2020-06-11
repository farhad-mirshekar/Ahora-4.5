USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyDeliveryDate'))
	DROP PROCEDURE app.spModifyDeliveryDate
GO

CREATE PROCEDURE app.spModifyDeliveryDate
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(1000),
@Description NVARCHAR(1000),
@Enabled TINYINT,
@IsNewRecord bit,
@UserID UNIQUEIDENTIFIER,
@Priority INT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[DeliveryDate]
				(ID,[Name],[Description],[Enabled]  ,[UserID], CreationDate,[Priority])
			VALUES
				(@ID, @Name , @Description , @Enabled  , @UserID , GETDATE() , @Priority)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[DeliveryDate]
			SET
				[Name] = @Name,
				[Description] = @Description,
				[Enabled] = @Enabled , 
				[UserID] = @UserID,
				[Priority] =@Priority
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END