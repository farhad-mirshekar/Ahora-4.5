USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsArticleComment'))
	DROP PROCEDURE ptl.spGetsArticleComment
GO

CREATE PROCEDURE ptl.spGetsArticleComment
@ArticleID UNIQUEIDENTIFIER,
@ParentID UNIQUEIDENTIFIER,
@CommentType TINYINT,
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
			ArticleComment.*,
			CONCAT(CreatorComment.FirstName , ' ' , CreatorComment.LastName) AS CreatorName,
			Article.Title AS ArticleTitle
		FROM 
			ptl.ArticleComment ArticleComment
		INNER JOIN
			ptl.Article Article ON ArticleComment.ArticleID = Article.ID
		INNER JOIN
			org.[User] CreatorComment ON ArticleComment.UserID = CreatorComment.ID
		WHERE
			Article.RemoverID IS NULL
		AND ArticleComment.RemoverID IS NULL
		AND (@ArticleID IS NULL OR ArticleComment.ArticleID = @ArticleID)
		AND (@ParentID IS NULL OR ArticleComment.ParentID = @ParentID)
		AND (@CommentType < 1 OR ArticleComment.CommentType = @CommentType)
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