USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCategoryMapDiscount'))
	DROP PROCEDURE app.spGetCategoryMapDiscount
GO

CREATE PROCEDURE app.spGetCategoryMapDiscount
@CategoryID UNIQUEIDENTIFIER,
@DiscountID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		*
	FROM 
		app.Category_Discount_Mapping CDM
	WHERE
		CDM.Active = 1
	AND	(@CategoryID IS NULL OR CDM.CategoryID = @CategoryID)
	AND (@DiscountID IS NULL OR CDM.DiscountID = @DiscountID)
END