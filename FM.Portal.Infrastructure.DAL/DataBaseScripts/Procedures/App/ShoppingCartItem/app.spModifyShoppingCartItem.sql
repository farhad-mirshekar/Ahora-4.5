USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyShoppingCartItem'))
	DROP PROCEDURE app.spModifyShoppingCartItem
GO

CREATE PROCEDURE app.spModifyShoppingCartItem
@ShoppingID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER,
@ProductID UNIQUEIDENTIFIER,
@Quantity int,
@AttributeJson Nvarchar(max),
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ShoppingCartItem]
				(ID,[ShoppingID],[UserID],[ProductID],[Quantity],[AttributeJson], CreationDate)
			VALUES
				(NEWID(),@ShoppingID , @UserID,@ProductID,@Quantity,@AttributeJson , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ShoppingCartItem]
			SET
				Quantity = @Quantity , 
				AttributeJson = @AttributeJson
			WHERE
				[ShoppingID] = @ShoppingID AND
				[ProductID] = @ProductID
		END

	RETURN @@ROWCOUNT
END