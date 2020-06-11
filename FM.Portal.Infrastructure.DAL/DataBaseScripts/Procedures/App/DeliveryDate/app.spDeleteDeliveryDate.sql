
USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('app.spDeleteDeliveryDate'))
	DROP PROCEDURE app.spDeleteDeliveryDate
GO

CREATE PROCEDURE app.spDeleteDeliveryDate
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	BEGIN TRAN
	UPDATE app.Product
	SET DeliveryDateID = NULL
	WHERE DeliveryDateID = @ID

	DELETE FROM app.DeliveryDate WHERE ID = @ID
	COMMIT;
END