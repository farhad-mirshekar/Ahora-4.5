USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetDynamicPage'))
	DROP PROCEDURE ptl.spGetDynamicPage
GO

CREATE PROCEDURE ptl.spGetDynamicPage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		dpages.*,
		pages.Name AS PageName
	FROM ptl.DynamicPage dpages
	INNER JOIN
		ptl.Pages pages ON dpages.PageID = pages.ID
	WHERE (dpages.ID = @ID)

	RETURN @@ROWCOUNT
END