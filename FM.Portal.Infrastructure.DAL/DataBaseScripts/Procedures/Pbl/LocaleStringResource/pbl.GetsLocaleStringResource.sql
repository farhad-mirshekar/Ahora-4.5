USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsLocaleStringResource'))
	DROP PROCEDURE pbl.spGetsLocaleStringResource
GO

CREATE PROCEDURE pbl.spGetsLocaleStringResource
@LanguageID UNIQUEIDENTIFIER,
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN
	SET @PageSize = COALESCE(@PageSize , 0)
	SET @PageIndex = COALESCE(@PageIndex,0)

	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END
	;WITH MainSelect AS
	(
		SELECT
			lsr.*
		FROM pbl.[LocaleStringResource] lsr
		WHERE
		  lsr.LanguageID = @LanguageID
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