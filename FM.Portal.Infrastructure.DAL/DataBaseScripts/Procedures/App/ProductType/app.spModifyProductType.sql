USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductType'))
	DROP PROCEDURE app.spModifyProductType
GO

CREATE PROCEDURE app.spModifyProductType
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(1000),
@Description NVARCHAR(1000),
@Enabled TINYINT,
@Price Decimal(18,3),
@IsNewRecord bit,
@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ProductType]
				(ID,[Name],[Description],[Enabled]  , [Price],[UserID], CreationDate)
			VALUES
				(@ID, @Name , @Description , @Enabled , @Price , @UserID , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ProductType]
			SET
				[Name] = @Name,
				[Description] = @Description,
				[Enabled] = @Enabled , 
				[Price]  = @Price ,
				[UserID] = @UserID
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END