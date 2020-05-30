USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spListProductShowOnHomePage'))
	DROP PROCEDURE app.spListProductShowOnHomePage
GO

CREATE PROCEDURE app.spListProductShowOnHomePage
@Count int
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
	LEFT JOIN
		app.Category_Discount_Mapping catMapdisc ON cat.ID = catMapdisc.CategoryID
	LEFT JOIN
	    app.Discount disc ON catMapdisc.DiscountID = disc.ID
	WHERE 
		catMapdisc.Active = 1
)
	SELECT
		Top (@Count)
		Product.TrackingCode,
		Product.DiscountType AS SelfProductDiscountType,
		Product.Discount AS SelfProductDiscount,
		Product.Price,
		product.Name,
		attachment.PathType,
		attachment.[FileName],
		discount.HasDiscountsApplied,
		discount.DiscountName,
		discount.DiscountAmount,
		discount.DiscountType
		
	FROM 
		app.Product product
	INNER JOIN 
		pbl.Attachment attachment ON product.ID = attachment.ParentID
	LEFT JOIN 
		Discount discount ON product.CategoryID = discount.ID
	WHERE
		attachment.[Type] = 1
END