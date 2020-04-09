USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetOrder'))
	DROP PROCEDURE app.spGetOrder
GO

CREATE PROCEDURE app.spGetOrder
@ID UNIQUEIDENTIFIER,
@ShoppingID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		orders.*
	FROM	
		[app].[Order] orders
	WHERE 
		(@ID IS NULL OR orders.ID = @ID) AND
		(@ShoppingID IS NULL OR orders.ShoppingID = @ShoppingID)
END