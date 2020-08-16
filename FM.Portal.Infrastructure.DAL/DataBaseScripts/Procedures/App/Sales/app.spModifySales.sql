USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifySales'))
	DROP PROCEDURE app.spModifySales
GO

CREATE PROCEDURE app.spModifySales
@ID UNIQUEIDENTIFIER,
@PaymentID UNIQUEIDENTIFIER,
@Type TINYINT,
@IsNewRecord BIT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Sales]
				(ID,[PaymentID],[Type],[CreationDate])
			VALUES
				(@ID, @PaymentID , @Type, GETDATE() )
		END
	RETURN @@ROWCOUNT
END