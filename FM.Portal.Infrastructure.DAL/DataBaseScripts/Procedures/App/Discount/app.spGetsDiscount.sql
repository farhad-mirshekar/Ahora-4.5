USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsDiscount'))
	DROP PROCEDURE app.spGetsDiscount
GO

CREATE PROCEDURE app.spGetsDiscount
@Name NVARCHAR(100),
@DiscountType TINYINT,
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
	;WITH MainSelect AS(
	SELECT 
		Discount.*
	FROM 
		app.Discount Discount
	WHERE
		(@Name IS NULL OR Discount.[Name] LIKE CONCAT('%' , @Name , '%'))
	AND (@DiscountType < 1 OR Discount.DiscountType = @DiscountType)
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)

	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END