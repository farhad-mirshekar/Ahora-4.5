USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProduct'))
	DROP PROCEDURE app.spGetsProduct
GO

CREATE PROCEDURE app.spGetsProduct
@CategoryID UNIQUEIDENTIFIER,
@ShowOnHomePage BIT,
@HasDiscount TINYINT,
@SpecialOffer BIT,
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN
	SET @PageSize = COALESCE(@PageSize , 5)
	SET @PageIndex = COALESCE(@PageIndex,1)

	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END
	;WITH MainSelect AS
	(
		SELECT 
			product.*,
			disc.[Name] AS DiscountName,
			disc.DiscountAmount,
			disc.DiscountType AS DiscountTypes,
			category.HasDiscountsApplied,
			category.Title AS CategoryName,
			ShippingCost.Name AS ShippingCostName,
			DeliveryDate.Name AS DeliveryDateName
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
		LEFT JOIN
			[app].[DeliveryDate] DeliveryDate ON product.DeliveryDateID = DeliveryDate.ID
		WHERE
			(@CategoryID IS NULL OR product.CategoryID = @CategoryID)
		AND (@ShowOnHomePage IS NULL OR product.ShowOnHomePage = @ShowOnHomePage)
		AND (@HasDiscount < 1 OR product.HasDiscount = @HasDiscount)
		AND (@SpecialOffer IS NULL OR product.SpecialOffer = @SpecialOffer)
	), TempCount AS 
	(
		SELECT COUNT(*) AS Total FROM MainSelect
	)

	SELECT * FROM MainSelect, TempCount						
	ORDER BY CreationDate DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION (RECOMPILE);
END