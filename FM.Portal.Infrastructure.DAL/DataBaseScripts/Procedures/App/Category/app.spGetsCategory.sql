USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsCategory'))
	DROP PROCEDURE app.spGetsCategory
GO

CREATE PROCEDURE app.spGetsCategory
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM app.Category
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END