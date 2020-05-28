USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsDynamicPage'))
	DROP PROCEDURE ptl.spGetsDynamicPage
GO

CREATE PROCEDURE [ptl].[spGetsDynamicPage]
@PageID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		dpages.*,
		pages.Name AS PageName,
		attachment.PathType,
		attachment.[FileName]
	FROM ptl.DynamicPage dpages
	INNER JOIN
		ptl.Pages pages ON dpages.PageID = pages.ID
	LEFT JOIN
		pbl.Attachment attachment ON dpages.ID = attachment.ParentID
	WHERE
		dpages.PageID = @PageID
	ORDER BY [CreationDate]

	RETURN @@ROWCOUNT
END