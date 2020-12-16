USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetEvents'))
	DROP PROCEDURE ptl.spGetEvents
GO

CREATE PROCEDURE ptl.spGetEvents
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		[Events].*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName
	FROM	
		[ptl].[Events] [Events]
	INNER JOIN
		[org].[User] creator ON [Events].UserID = creator.ID
	INNER JOIN 
		ptl.Category Category ON [Events].CategoryID = Category.ID
	WHERE 
		[Events].RemoverID IS NULL
	AND [Events].ID = @ID
	AND Category.RemoverID IS NULL
END