USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spListFaqGroup'))
	DROP PROCEDURE app.spListFaqGroup
GO

CREATE PROCEDURE app.spListFaqGroup
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
	SELECT 
		faqgroup.Title,
		faqgroup.ID,
		faqgroup.CreationDate,
		count(*) AS Total
	FROM 
		app.FAQGroup faqgroup
	INNER JOIN
		app.FAQ faq ON faqgroup.ID = faq.FAQGroupID 
	GROUP BY 
		faqgroup.Title ,faqgroup.CreationDate,faqgroup.ID
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END