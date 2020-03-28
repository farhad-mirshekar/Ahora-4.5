USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsDiscount'))
	DROP PROCEDURE app.spGetsDiscount
GO

CREATE PROCEDURE app.spGetsDiscount
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM app.Discount
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END