USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyShippingCost'))
	DROP PROCEDURE app.spModifyShippingCost
GO

CREATE PROCEDURE app.spModifyShippingCost
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(1000),
@Description NVARCHAR(1000),
@Enabled TINYINT,
@Price Decimal(18,3),
@IsNewRecord bit,
@UserID UNIQUEIDENTIFIER,
@Priority INT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ShippingCost]
				(ID,[Name],[Description],[Enabled]  , [Price],[UserID], CreationDate,[Priority])
			VALUES
				(@ID, @Name , @Description , @Enabled , @Price , @UserID , GETDATE() , @Priority)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ShippingCost]
			SET
				[Name] = @Name,
				[Description] = @Description,
				[Enabled] = @Enabled , 
				[Price]  = @Price ,
				[UserID] = @UserID,
				[Priority] =@Priority
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END