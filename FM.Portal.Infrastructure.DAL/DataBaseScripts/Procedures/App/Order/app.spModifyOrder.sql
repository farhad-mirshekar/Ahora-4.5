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
@Price decimal(18,3),
@IsNewRecord BIT,
@TrackingCode Nvarchar(MAX)
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
		BEGIN
			INSERT INTO [app].[Order]
				(ID,[ShoppingID],[UserID], [SendType],[BankID],[CreationDate], AddressID,Price)
			VALUES
				(@ID,@ShoppingID , @UserID , @SendType , @BankID , GETDATE(),@AddressID , @Price)
		END
		ELSE
			UPDATE 
				[app].[Order]
			SET
				TrackingCode = @TrackingCode
			WHERE 
				ID = @ID

	RETURN @@ROWCOUNT
END