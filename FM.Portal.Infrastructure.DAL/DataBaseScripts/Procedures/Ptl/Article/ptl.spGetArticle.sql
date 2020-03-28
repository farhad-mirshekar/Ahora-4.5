USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetArticle'))
	DROP PROCEDURE ptl.spGetArticle
GO

CREATE PROCEDURE ptl.spGetArticle
@ID UNIQUEIDENTIFIER,
@TrackingCode NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		article.*,
		CONCAT(creator.FirstName,' ' , creator.LastName) AS CreatorName,
		attachment.[FileName],
		attachment.PathType
	FROM	
		[ptl].[Article] article
	INNER JOIN
		[org].[User] creator ON article.UserID = creator.ID
	LEFT JOIN 
		pbl.Attachment attachment ON article.ID = attachment.ParentID
	WHERE 
		article.RemoverID IS NULL AND
		(@ID IS NULL OR article.ID = @ID) AND
		(@TrackingCode IS NULL OR article.TrackingCode = @TrackingCode)
END