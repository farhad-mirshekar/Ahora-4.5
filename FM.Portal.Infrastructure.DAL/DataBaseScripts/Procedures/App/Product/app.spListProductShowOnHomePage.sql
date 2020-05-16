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
	SELECT
		Top (@Count)
		Product.TrackingCode,
		Product.DiscountType AS SelfProductDiscountType,
		Product.Discount AS SelfProductDiscount,
		Product.Price,
		product.Name,
		attachment.PathType,
		attachment.[FileName],
		cat.HasDiscountsApplied,
		disc.[Name] AS DiscountName,
		disc.DiscountAmount,
		disc.DiscountPercentage,
		disc.DiscountType
		
	FROM 
		app.Product product
	INNER JOIN 
		pbl.Attachment attachment ON product.ID = attachment.ParentID
	INNER JOIN
		app.Category cat ON product.CategoryID = cat.ID
	LEFT JOIN
		app.Category_Discount_Mapping catMapdisc ON cat.ID = catMapdisc.CategoryID
	LEFT JOIN
	    app.Discount disc ON catMapdisc.DiscountID = disc.ID
	WHERE
		attachment.[Type] = 1
END