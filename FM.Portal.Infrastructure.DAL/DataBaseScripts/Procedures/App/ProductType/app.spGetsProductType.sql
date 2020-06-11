USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductType'))
	DROP PROCEDURE app.spGetsProductType
GO

CREATE PROCEDURE app.spGetsProductType
@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		ProductType.*
	FROM app.ProductType ProductType
	WHERE
		(@Enabled IS NULL OR ProductType.[Enabled] = @Enabled)
	ORDER BY ProductType.[CreationDate] DESC
END