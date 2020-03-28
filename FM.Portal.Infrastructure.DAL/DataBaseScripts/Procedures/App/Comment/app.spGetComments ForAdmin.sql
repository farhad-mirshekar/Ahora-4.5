USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCommentsForAdmin'))
	DROP PROCEDURE app.spGetCommentsForAdmin
GO

CREATE PROCEDURE app.spGetCommentsForAdmin

--WITH ENCRYPTION
AS
BEGIN

;WITH main AS(
	SELECT 
		comment.*,
		CONCAT(users.FirstName ,' ',users.LastName) AS NameFamily,
		product.Name AS ProductName
	FROM	
		[app].[Comment] comment
	INNER JOIN
		[org].[User] users ON comment.UserID = users.ID
		LEFT JOIN
		app.Product product ON comment.DocumentID = product.ID
		)

	SELECT * FROM main ORDER BY CreationDate
END