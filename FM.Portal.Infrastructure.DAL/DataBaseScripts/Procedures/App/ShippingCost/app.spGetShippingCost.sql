USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetShippingCost'))
	DROP PROCEDURE app.spGetShippingCost
GO

CREATE PROCEDURE app.spGetShippingCost
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		ShippingCost.*
	FROM	
		[app].[ShippingCost] ShippingCost
	WHERE 
		ShippingCost.ID = @ID
END