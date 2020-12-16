USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetNews'))
	DROP PROCEDURE ptl.spGetNews
GO

CREATE PROCEDURE ptl.spGetNews
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		news.*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName
	FROM	
		[ptl].[News] news
	INNER JOIN
		[org].[User] creator ON news.UserID = creator.ID
	INNER JOIN
		ptl.Category Category ON news.CategoryID = Category.ID
	WHERE 
		news.RemoverID IS NULL 
	AND news.ID = @ID
	AND Category.RemoverID IS NULL
END