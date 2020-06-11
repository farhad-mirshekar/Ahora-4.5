USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsShippingCost'))
	DROP PROCEDURE app.spGetsShippingCost
GO

CREATE PROCEDURE app.spGetsShippingCost
@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		ShippingCost.*
	FROM app.ShippingCost ShippingCost
	WHERE
		(@Enabled IS NULL OR ShippingCost.[Enabled] = @Enabled)
	ORDER BY ShippingCost.[CreationDate] DESC
END