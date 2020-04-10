USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetPayment'))
	DROP PROCEDURE app.spGetPayment
GO

CREATE PROCEDURE app.spGetPayment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		payment.*
	FROM	
		[app].[Payment] payment
	WHERE 
		payment.ID = @ID
END