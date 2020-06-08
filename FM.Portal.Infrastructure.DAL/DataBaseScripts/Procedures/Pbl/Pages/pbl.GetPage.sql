USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetPage'))
	DROP PROCEDURE pbl.spGetPage
GO

CREATE PROCEDURE pbl.spGetPage
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;


	SELECT pages.ID,
		pages.[Node].ToString() Node,
		pages.[Node].GetAncestor(1).ToString() ParentNode,
		pages.ControllerName,
		pages.ActionName,
		pages.Title,
		Pages.[Description],
		pages.[Enabled],
		pages.[Deleted],
		pages.RouteUrl,
		pages.CreationDate
	FROM pbl.Pages pages
	WHERE (ID = @ID)

	RETURN @@ROWCOUNT
END