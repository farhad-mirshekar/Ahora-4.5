USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsFaqGroup'))
	DROP PROCEDURE app.spGetsFaqGroup
GO

CREATE PROCEDURE app.spGetsFaqGroup
@Title NVARCHAR(100),
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
	;WITH Faq AS
	(
		SELECT 
			FAQGroupID,
			Count(*) AS CountFaq
		FROM 
			app.FAQ
		GROUP BY
			FAQGroupID
	), MainSelect AS
	(
		SELECT 
			FAQGroup.*,
			faq.CountFaq
		FROM
			app.FAQGroup FAQGroup
		INNER JOIN
			Faq faq ON FAQGroup.ID = faq.FAQGroupID 
		WHERE
			(@Title IS NULL OR FAQGroup.Title LIKE CONCAT('%' , @Title , '%'))
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