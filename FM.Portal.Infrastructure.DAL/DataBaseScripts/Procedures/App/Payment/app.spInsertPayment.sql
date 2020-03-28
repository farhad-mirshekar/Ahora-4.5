USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spInsertPayment'))
	DROP PROCEDURE app.spInsertPayment
GO

CREATE PROCEDURE app.spInsertPayment
@ID UNIQUEIDENTIFIER,
@OrderID UNIQUEIDENTIFIER,
@UserID UNIQUEIDENTIFIER,
@Price decimal(18,3),
@Token nvarchar(max)
--WITH ENCRYPTION
AS
BEGIN
			INSERT INTO [app].[Payment]
				(ID,[OrderID],[UserID], [Price],[Token],[CreationDate])
			VALUES
				(@ID,@OrderID , @UserID , @Price , @Token , GETDATE())
	RETURN @@ROWCOUNT
END