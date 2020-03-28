USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyOrder'))
	DROP PROCEDURE app.spModifyOrder
GO

CREATE PROCEDURE app.spModifyOrder
@ID UNIQUEIDENTIFIER,
@ShoppingID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER,
@SendType tinyint,
@BankID UNIQUEIDENTIFIER,
@AddressID UNIQUEIDENTIFIER,
@Price decimal(18,3)
--WITH ENCRYPTION
AS
BEGIN
			DECLARE @count int;
			SET @count = (SELECT COUNT(*) FROM app.[Order])
			SET @count = @count + 1; 
			INSERT INTO [app].[Order]
				(ID,[ShoppingID],[UserID], [SendType],[BankID],[CreationDate], AddressID,Price,TrackingCode)
			VALUES
				(@ID,@ShoppingID , @UserID , @SendType , @BankID , GETDATE(),@AddressID , @Price , @count)
	RETURN @@ROWCOUNT
END