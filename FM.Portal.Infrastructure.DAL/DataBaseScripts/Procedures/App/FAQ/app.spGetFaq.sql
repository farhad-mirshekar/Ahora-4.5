USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetFaq'))
	DROP PROCEDURE app.spGetFaq
GO

CREATE PROCEDURE app.spGetFaq
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		faq.*
	FROM	
		[app].[FAQ] faq
	WHERE 
		faq.ID = @ID
END