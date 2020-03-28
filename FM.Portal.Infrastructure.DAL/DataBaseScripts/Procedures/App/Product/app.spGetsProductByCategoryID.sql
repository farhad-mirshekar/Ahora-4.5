USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductByCategoryID'))
	DROP PROCEDURE app.spGetsProductByCategoryID
GO

CREATE PROCEDURE app.spGetsProductByCategoryID
--WITH ENCRYPTION
@CategoryID UNIQUEIDENTIFIER
AS
BEGIN
	;WITH main AS(
	SELECT 
		product.ID AS ProductID,
		Product.[Name] AS ProductName,
		category.Title AS CategoryName,
		attachment.[FileName],
		product.CreationDate
	FROM 
		app.Product product
	INNER JOIN	
		app.Category category ON product.CategoryID = category.ID
	LEFT JOIN
		pbl.Attachment attachment ON product.ID = attachment.ParentID
	WHERE
		(@CategoryID IS NULL OR category.ID = @CategoryID) AND
		attachment.[Type] = 1
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END