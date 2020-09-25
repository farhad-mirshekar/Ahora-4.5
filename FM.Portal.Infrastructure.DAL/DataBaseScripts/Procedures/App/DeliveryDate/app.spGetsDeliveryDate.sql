USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsDeliveryDate'))
	DROP PROCEDURE app.spGetsDeliveryDate
GO

CREATE PROCEDURE app.spGetsDeliveryDate
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
			DeliveryDate.*
		FROM app.DeliveryDate DeliveryDate
		WHERE
			(@Enabled < 1 OR DeliveryDate.[Enabled] = @Enabled)
		AND (@Priority IS NULL OR DeliveryDate.[Priority] = @Priority)
		AND (@Name IS NULL OR DeliveryDate.[Name] LIKE CONCAT('%' , @Name , '%')) 
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