USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetDeliveryDate'))
	DROP PROCEDURE app.spGetDeliveryDate
GO

CREATE PROCEDURE app.spGetDeliveryDate
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		DeliveryDate.*
	FROM	
		[app].[DeliveryDate] DeliveryDate
	WHERE 
		DeliveryDate.ID = @ID
END