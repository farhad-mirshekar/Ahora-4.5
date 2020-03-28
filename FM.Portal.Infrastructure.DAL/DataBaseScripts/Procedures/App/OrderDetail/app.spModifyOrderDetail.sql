USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyOrderDetail'))
	DROP PROCEDURE app.spModifyOrderDetail
GO

CREATE PROCEDURE app.spModifyOrderDetail
@ID UNIQUEIDENTIFIER,
@OrderID UNIQUEIDENTIFIER,
@ProductJson NVARCHAR(MAX),
@UserJson NVARCHAR(MAX),
@AttributeJson NVARCHAR(MAX)
--WITH ENCRYPTION
AS
BEGIN
			INSERT INTO [app].[OrderDetail]
				(ID,[OrderID],[ProductJson],[AttributeJson] ,[UserJson])
			VALUES
				(@ID,@OrderID , @ProductJson , @AttributeJson , @UserJson)
	RETURN @@ROWCOUNT
END