USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetEventsComment'))
	DROP PROCEDURE ptl.spGetEventsComment
GO

CREATE PROCEDURE ptl.spGetEventsComment
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		EventsComment.*,
		[Events].Title AS EventsTitle
	FROM	
		[ptl].[EventsComment] EventsComment
	INNER JOIN
		ptl.[Events] [Events] ON EventsComment.EventsID = [Events].ID
	WHERE 
		EventsComment.ID = @ID
	AND EventsComment.RemoverID IS NULL
	AND [Events].RemoverID IS NULL
END