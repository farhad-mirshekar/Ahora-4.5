USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetComments'))
	DROP PROCEDURE app.spGetComments
GO

CREATE PROCEDURE app.spGetComments
@DocumentID UNIQUEIDENTIFIER,
@PageSize int,
@PageIndex int
--WITH ENCRYPTION
AS
BEGIN
IF @PageIndex = 0 
	BEGIN
		SET @pagesize = 10000000
		SET @PageIndex = 1
	END
;WITH main AS(
	SELECT 
		comment.*
	FROM	
		[app].[Comment] comment
	WHERE 
		comment.DocumentID = @DocumentID AND
		comment.CommentType = 2)

	SELECT * FROM main ORDER BY CreationDate
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
END