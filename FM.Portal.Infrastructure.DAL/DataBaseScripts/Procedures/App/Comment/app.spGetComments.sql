USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetComments'))
	DROP PROCEDURE app.spGetComments
GO

CREATE PROCEDURE app.spGetComments
@DocumentID UNIQUEIDENTIFIER,
@ParentID UNIQUEIDENTIFIER,
@CommentType TINYINT,
@CommentForType TINYINT,
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN
	SET @PageSize = COALESCE(@PageSize , 5)
	SET @PageIndex = COALESCE(@PageIndex,1)

	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END

	;WITH MainSelect AS
	(
		SELECT 
			comment.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorName,
			COALESCE(Product.[Name] , Article.Title , News.Title , [Events].Title) AS ProductName
		FROM	
			[app].[Comment] comment
		INNER JOIN
			org.[User] CreatorUser ON comment.UserID = CreatorUser.ID
		LEFT JOIN
			app.Product Product ON comment.DocumentID = Product.ID
		LEFT JOIN
			ptl.Article Article ON comment.DocumentID = Article.ID
		LEFT JOIN
			ptl.News News ON comment.DocumentID = News.ID
		LEFT JOIN
			ptl.[Events] [Events] ON comment.DocumentID = [Events].ID
		WHERE
			(@DocumentID IS NULL OR comment.DocumentID = @DocumentID)
		AND (@ParentID IS NULL OR comment.ParentID = @ParentID)
		AND (@CommentType < 1 OR comment.CommentType = @CommentType)
		AND (@CommentForType < 1 OR comment.CommentForType = @CommentForType)
	),TemptCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM
			MainSelect
	)

	SELECT 
		*
	FROM 
		MainSelect,TemptCount 
	ORDER BY
		CreationDate DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE);
END