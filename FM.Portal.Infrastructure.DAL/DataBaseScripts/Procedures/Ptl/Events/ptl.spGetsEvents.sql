USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsEvents'))
	DROP PROCEDURE ptl.spGetsEvents
GO

CREATE PROCEDURE ptl.spGetsEvents
@Title AS NVARCHAR(100),
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
			[Events].*,
			CONCAT(CreatorUser.FirstName , ' ' ,CreatorUser.LastName) AS CreatorName
		FROM ptl.[Events] [Events]
		INNER JOIN 
			org.[User] CreatorUser ON [Events].UserID = CreatorUser.ID
		INNER JOIN 
			ptl.Category Category ON [Events].CategoryID = Category.ID
		LEFT JOIN
			pbl.[Language] lng ON [Events].LanguageID = lng.ID
		WHERE
			[Events].RemoverID IS NULL
		AND	Category.RemoverID IS NULL
		AND	(@Title IS NULL OR [Events].Title LIKE CONCAT('%', @Title , '%'))
		AND (@LanguageID IS NULL OR [Events].LanguageID = @LanguageID)
		AND (@ViewStatusType < 1 OR [Events].ViewStatusType = @ViewStatusType)
	),TempCount AS
	(
		SELECT 
			COUNT(*) AS Total
		FROM
			MainSelect
	)

	SELECT * 
	FROM MainSelect , TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END