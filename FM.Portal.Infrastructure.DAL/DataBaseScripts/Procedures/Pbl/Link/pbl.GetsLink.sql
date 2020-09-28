USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsLink'))
	DROP PROCEDURE pbl.spGetsLink
GO

CREATE PROCEDURE pbl.spGetsLink
@ShowFooter BIT,
@Enabled TINYINT,
@Name NVARCHAR(100),
@Priority INT,
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
			link.*
		FROM pbl.Link link
		WHERE
			(@ShowFooter IS NULL OR link.ShowFooter = @ShowFooter)
		AND (@Name IS NULL OR link.[Name] LIKE CONCAT('%' , @Name , '%'))
		AND (@Priority IS NULL OR link.[Priority] = @Priority)
		AND (@Enabled < 1 OR link.[Enabled] = @Enabled)
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