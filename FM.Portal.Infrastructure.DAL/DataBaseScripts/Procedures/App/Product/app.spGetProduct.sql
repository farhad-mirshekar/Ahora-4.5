USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProduct'))
	DROP PROCEDURE app.spGetProduct
GO

CREATE PROCEDURE app.spGetProduct
@ID UNIQUEIDENTIFIER,
@TrackingCode NVARCHAR(20)
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		product.*,
		disc.[Name] AS DiscountName,
		disc.DiscountAmount,
		disc.DiscountType AS DiscountTypes,
		category.HasDiscountsApplied,
		ShippingCost.Name AS ShippingCostName
	FROM	
		[app].[Product] product
	INNER JOIN
		[app].[Category] category ON product.CategoryID = category.ID
	LEFT JOIN
		[app].[Category_Discount_Mapping] catMapdisc ON category.ID = catMapdisc.CategoryID
	LEFT JOIN
		[app].[Discount] disc ON catMapdisc.DiscountID =disc.ID
	LEFT JOIN
		[app].[ShippingCost] ShippingCost ON product.ShippingCostID = ShippingCost.ID
	WHERE 
		(@ID IS NULL OR product.ID = @ID) AND
		(@TrackingCode IS NULL OR product.TrackingCode = @TrackingCode)
END