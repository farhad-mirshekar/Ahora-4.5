USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsFaq'))
	DROP PROCEDURE app.spGetsFaq
GO

CREATE PROCEDURE app.spGetsFaq
@FAQGroupID UNIQUEIDENTIFIER,
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
			faq.*,
			FaqGroup.Title AS FaqGroupTitle
		FROM 
			[app].[FAQ] faq
		INNER JOIN 
			app.FAQGroup FaqGroup ON faq.FAQGroupID = FaqGroup.ID
		WHERE
			faq.FAQGroupID = @FAQGroupID
	),tempCount AS
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