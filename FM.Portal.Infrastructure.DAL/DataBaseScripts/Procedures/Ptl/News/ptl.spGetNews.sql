USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetNews'))
	DROP PROCEDURE ptl.spGetNews
GO

CREATE PROCEDURE ptl.spGetNews
@ID UNIQUEIDENTIFIER,
@TrackingCode NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		news.*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName,
		attachment.[FileName],
		attachment.PathType
	FROM	
		[ptl].[News] news
	INNER JOIN
		[org].[User] creator ON news.UserID = creator.ID
	LEFT JOIN
		[pbl].[Attachment] attachment ON news.ID = attachment.ParentID
	WHERE 
		news.RemoverID IS NULL AND
		(@ID IS NULL OR news.ID = @ID) AND
		(@TrackingCode IS NULL OR news.TrackingCode = @TrackingCode)
END