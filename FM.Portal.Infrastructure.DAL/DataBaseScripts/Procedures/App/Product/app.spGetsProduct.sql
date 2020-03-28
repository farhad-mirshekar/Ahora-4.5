USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProduct'))
	DROP PROCEDURE app.spGetsProduct
GO

CREATE PROCEDURE app.spGetsProduct
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM app.Product
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END