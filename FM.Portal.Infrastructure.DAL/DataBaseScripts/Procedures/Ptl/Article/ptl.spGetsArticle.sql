USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsArticle'))
	DROP PROCEDURE ptl.spGetsArticle
GO

CREATE PROCEDURE ptl.spGetsArticle
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
			Article.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorUserFullName,
			Category.Title AS CategoryName
		FROM 
			ptl.Article Article
		INNER JOIN org.[User] CreatorUser ON Article.UserID = CreatorUser.ID
		LEFT JOIN ptl.Category Category ON Article.CategoryID = Category.ID
		LEFT JOIN pbl.[Language] lng ON Article.LanguageID = lng.ID
		WHERE
			Category.RemoverID IS NULL
		AND	(@Title IS NULL OR Article.Title LIKE CONCAT('%', @Title , '%'))
		AND (@LanguageID IS NULL OR Article.LanguageID = @LanguageID)
		AND (@ViewStatusType < 1 OR Article.ViewStatusType = @ViewStatusType)
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM MainSelect
	)

	SELECT * 
	FROM MainSelect , TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END