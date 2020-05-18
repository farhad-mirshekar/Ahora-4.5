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
	;WITH main AS(
	SELECT 
		cart.*,
		cat.HasDiscountsApplied,
		disc.Name AS DiscountName,
		disc.DiscountAmount,
		disc.DiscountType,
		product.HasDiscount,
		product.Discount AS SelfProductDiscountAmount,
		product.DiscountType AS SelfProductDiscountType
	FROM 
		[app].[ShoppingCartItem] cart
	INNER JOIN	
		[app].[Product] product ON cart.ProductID = product.ID
	LEFT JOIN
		[app].[Category] cat ON product.CategoryID = cat.ID
	LEFT JOIN
		[app].[Category_Discount_Mapping] catMapdisc ON cat.ID = catMapdisc.CategoryID
	LEFT JOIN
		[app].[Discount] disc ON catMapdisc.DiscountID = disc.ID
	WHERE 
		[ShoppingID]=@ShoppingID
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END