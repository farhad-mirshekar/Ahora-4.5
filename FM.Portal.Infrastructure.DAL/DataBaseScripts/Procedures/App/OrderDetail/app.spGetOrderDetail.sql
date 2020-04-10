USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetOrderDetail'))
	DROP PROCEDURE app.spGetOrderDetail
GO

CREATE PROCEDURE app.spGetOrderDetail
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		detail.*
	FROM	
		[app].[OrderDetail] detail
	WHERE 
		detail.OrderID = @ID
END