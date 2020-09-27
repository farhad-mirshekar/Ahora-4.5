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
@OnlyProduct BIT,
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

	;WITH ProductComment AS
	(
		SELECT 
			comment.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorName,
			COALESCE(Product.[Name],N'وارد نشده است') AS ProductName
		FROM	
			[app].[Comment] comment
		INNER JOIN
			org.[User] CreatorUser ON comment.UserID = CreatorUser.ID
		LEFT JOIN
			app.Product Product ON comment.DocumentID = Product.ID 
		WHERE
			comment.CommentForType = 6
		AND @OnlyProduct = 1
	), OthersComment AS
	(
		SELECT 
			comment.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorName,
			COALESCE(Article.Title , News.Title , [Events].Title,N'وارد نشده است') AS ProductName

		FROM	
			[app].[Comment] comment
		INNER JOIN
			org.[User] CreatorUser ON comment.UserID = CreatorUser.ID
		LEFT JOIN
			ptl.Article Article ON comment.DocumentID = Article.ID
		LEFT JOIN
			ptl.News News ON comment.DocumentID = News.ID
		LEFT JOIN
			ptl.[Events] [Events] ON comment.DocumentID = [Events].ID
		WHERE
			comment.CommentForType <> 6
		AND @OnlyProduct <> 1
	),Aggregated AS
	(
		SELECT 
			*
		FROM ProductComment
		UNION ALL
		SELECT
			*
		FROM OthersComment
	), MainSelect AS
	(
		SELECT 
			Aggregated.*
		FROM	
			Aggregated
		WHERE
			(@DocumentID IS NULL OR Aggregated.DocumentID = @DocumentID)
		AND (@ParentID IS NULL OR Aggregated.ParentID = @ParentID)
		AND (@CommentType < 1 OR Aggregated.CommentType = @CommentType)
		AND (@CommentForType < 1 OR Aggregated.CommentForType = @CommentForType)
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