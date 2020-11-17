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
		Events.*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName,
		attachment.[FileName],
		attachment.PathType
	FROM	
		[ptl].[Events] Events
	INNER JOIN
		[org].[User] creator ON Events.UserID = creator.ID
	LEFT JOIN
		[pbl].[Attachment] attachment ON Events.ID = attachment.ParentID
	WHERE 
		Events.RemoverID IS NULL
		AND Events.ID = @ID
END