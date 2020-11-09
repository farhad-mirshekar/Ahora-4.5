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
		EventsComment.*
	FROM	
		[ptl].[EventsComment] EventsComment
	WHERE 
		EventsComment.ID = @ID
END