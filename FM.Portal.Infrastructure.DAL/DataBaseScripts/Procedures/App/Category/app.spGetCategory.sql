USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCategory'))
	DROP PROCEDURE app.spGetCategory
GO

CREATE PROCEDURE app.spGetCategory
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.*,
		map.DiscountID
	FROM	
		[app].[Category] cat
	LEFT JOIN
		[app].[Category_Discount_Mapping] map ON cat.ID = map.CategoryID
	WHERE 
		cat.ID = @ID
END