USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsNews'))
	DROP PROCEDURE ptl.spGetsNews
GO

CREATE PROCEDURE ptl.spGetsNews
@Title NVARCHAR(100),
@LanguageID UNIQUEIDENTIFIER,
@ViewStatusType TINYINT,
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
			News.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorName
		FROM ptl.News News
		INNER JOIN 
			org.[User] CreatorUser ON News.UserID = CreatorUser.ID
		INNER JOIN
			ptl.Category Category ON News.CategoryID = Category.ID
		LEFT JOIN
			pbl.[Language] lng ON News.LanguageID = lng.ID
		WHERE
			News.RemoverID IS NULL
		AND Category.RemoverID IS NULL
		AND (@Title IS NULL OR News.Title LIKE CONCAT ('%' , @Title , '%'))
		AND (@LanguageID IS NULL OR News.LanguageID = @LanguageID)
		AND (@ViewStatusType < 1 OR News.ViewStatusType = @ViewStatusType)
	), TempCount AS
	(
		SELECT
		 COUNT(*) AS Total
		FROM MainSelect
	)

	SELECT * 
	FROM MainSelect , TempCount
	ORDER BY [CreationDate] DESC
	OFFSET((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END