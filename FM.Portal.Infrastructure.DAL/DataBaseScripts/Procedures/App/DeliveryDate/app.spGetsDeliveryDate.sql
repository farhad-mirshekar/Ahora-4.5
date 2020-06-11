USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsDeliveryDate'))
	DROP PROCEDURE app.spGetsDeliveryDate
GO

CREATE PROCEDURE app.spGetsDeliveryDate
@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		DeliveryDate.*
	FROM app.DeliveryDate DeliveryDate
	WHERE
		(@Enabled IS NULL OR DeliveryDate.[Enabled] = @Enabled)
	ORDER BY DeliveryDate.[CreationDate] DESC
END