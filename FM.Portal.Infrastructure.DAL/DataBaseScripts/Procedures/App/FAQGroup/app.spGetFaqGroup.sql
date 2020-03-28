USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetFaqGroup'))
	DROP PROCEDURE app.spGetFaqGroup
GO

CREATE PROCEDURE app.spGetFaqGroup
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		faq.*
	FROM	
		[app].[FAQGroup] faq
	WHERE 
		faq.ID = @ID
END