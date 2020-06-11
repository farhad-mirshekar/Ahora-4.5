USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsShoppingCartItem'))
	DROP PROCEDURE app.spGetsShoppingCartItem
GO

CREATE PROCEDURE app.spGetsShoppingCartItem
@ShoppingID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN
;WITH Discount AS
(
	SELECT 
		cat.ID,
		cat.HasDiscountsApplied,
		disc.[Name] AS DiscountName,
		disc.DiscountAmount,
		disc.DiscountType
	FROM app.Category cat
	INNER JOIN
		app.Category_Discount_Mapping catMapdisc ON cat.ID = catMapdisc.CategoryID
	INNER JOIN
	    app.Discount disc ON catMapdisc.DiscountID = disc.ID
	WHERE 
		catMapdisc.Active = 1
)
	SELECT 
		cart.*,
		disc.HasDiscountsApplied,
		disc.DiscountName,
		disc.DiscountAmount,
		disc.DiscountType,
		product.HasDiscount,
		product.Discount AS SelfProductDiscountAmount,
		product.DiscountType AS SelfProductDiscountType,
		ShippingCost.Price AS ShippingCostPrice,
		ShippingCost.Name AS ShippingCostName,
		ShippingCost.[Priority] AS ShippingCostPriority
	FROM 
		[app].[ShoppingCartItem] cart
	INNER JOIN	
		[app].[Product] product ON cart.ProductID = product.ID
	LEFT JOIN
		Discount disc ON product.CategoryID = disc.ID
	LEFT JOIN
		[app].[ShippingCost] ShippingCost ON product.ShippingCostID = ShippingCost.ID
	WHERE 
		[ShoppingID]=@ShoppingID
END