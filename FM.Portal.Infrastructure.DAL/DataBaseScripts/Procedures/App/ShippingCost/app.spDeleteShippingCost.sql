
USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('app.spDeleteShippingCost'))
	DROP PROCEDURE app.spDeleteShippingCost
GO

CREATE PROCEDURE app.spDeleteShippingCost
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM app.ShippingCost WHERE ID = @ID
END