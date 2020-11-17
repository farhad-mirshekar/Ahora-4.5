USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsUrlRecord'))
	DROP PROCEDURE pbl.spGetsUrlRecord
GO
CREATE PROCEDURE [pbl].spGetsUrlRecord
@UrlDesc NVARCHAR(MAX),
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
			UrlRecord.*
		FROM 
			pbl.UrlRecord UrlRecord
		WHERE
			(@UrlDesc IS NULL OR UrlRecord.UrlDesc LIKE CONCAT('%',@UrlDesc,'%'))
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
GO


