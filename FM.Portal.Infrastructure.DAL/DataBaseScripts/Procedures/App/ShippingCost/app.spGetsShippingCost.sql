USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsShippingCost'))
	DROP PROCEDURE app.spGetsShippingCost
GO

CREATE PROCEDURE app.spGetsShippingCost
@Enabled TINYINT,
@Priority INT,
@Name NVARCHAR(100),
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
			ShippingCost.*
		FROM app.ShippingCost ShippingCost
		WHERE
			(@Enabled IS NULL OR ShippingCost.[Enabled] = @Enabled)
		AND ((@Priority = 0 OR @Priority IS NULL) OR ShippingCost.[Priority] = @Priority)
		AND (@Name IS NULL OR ShippingCost.Name LIKE CONCAT('%' , @Name , '%'))
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM
			MainSelect
	)

	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END