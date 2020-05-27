USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsPage'))
	DROP PROCEDURE ptl.spGetsPage
GO

CREATE PROCEDURE ptl.spGetsPage
@PageType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		pages.*
	FROM ptl.Pages pages
	WHERE
		(@PageType IS NULL OR pages.PageType = @PageType)
	ORDER BY [CreationDate]

	RETURN @@ROWCOUNT
END