USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductAttribute'))
	DROP PROCEDURE app.spGetsProductAttribute
GO

CREATE PROCEDURE app.spGetsProductAttribute
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM app.ProductAttribute
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END