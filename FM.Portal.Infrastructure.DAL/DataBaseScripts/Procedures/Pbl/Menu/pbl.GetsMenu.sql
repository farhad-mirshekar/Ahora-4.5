USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsMenu'))
	DROP PROCEDURE pbl.spGetsMenu
GO

CREATE PROCEDURE pbl.spGetsMenu
@LanguageID UNIQUEIDENTIFIER,
@Name NVARCHAR(256),
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
			menu.*
		FROM pbl.Menu menu
		WHERE
			menu.RemoverID IS NULL
		AND (@LanguageID IS NULL OR menu.LanguageID = @LanguageID)
		AND (@Name IS NULL OR menu.[Name] LIKE CONCAT('%' , @Name , '%'))
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