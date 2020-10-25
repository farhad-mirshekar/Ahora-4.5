USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsLanguage'))
	DROP PROCEDURE pbl.spGetsLanguage
GO

CREATE PROCEDURE pbl.spGetsLanguage
@Name NVARCHAR(3000),
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
			lng.*
		FROM pbl.[Language] lng
		WHERE
		  (@Name IS NULL OR lng.[Name] LIKE CONCAT('%' , @Name , '%'))
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