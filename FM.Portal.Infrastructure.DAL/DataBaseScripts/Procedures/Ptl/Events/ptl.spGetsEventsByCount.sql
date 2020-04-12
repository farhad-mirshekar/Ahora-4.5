USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsEventsByCount'))
	DROP PROCEDURE ptl.spGetsEventsByCount
GO

CREATE PROCEDURE ptl.spGetsEventsByCount
@count int
--WITH ENCRYPTION
AS
BEGIN
	;WITH main AS(
		SELECT 
		Events.*,
		attachment.[FileName],
		attachment.PathType
	FROM 
		ptl.Events Events
	LEFT JOIN 
		pbl.Attachment attachment ON Events.ID = attachment.ParentID
	WHERE 
		Events.RemoverID IS NULL AND
		attachment.PathType != 7 OR attachment.PathType IS NULL -- not show video in list
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END