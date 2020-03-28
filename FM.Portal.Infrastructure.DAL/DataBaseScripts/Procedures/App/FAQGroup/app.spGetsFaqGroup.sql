USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsFaqGroup'))
	DROP PROCEDURE app.spGetsFaqGroup
GO

CREATE PROCEDURE app.spGetsFaqGroup
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT *
	FROM app.FAQGroup
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END