USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsNewsComment'))
	DROP PROCEDURE ptl.spGetsNewsComment
GO

CREATE PROCEDURE ptl.spGetsNewsComment
@NewsID UNIQUEIDENTIFIER,
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
			NewsComment.*,
			CONCAT(CreatorComment.FirstName , ' ' , CreatorComment.LastName) AS CreatorName
		FROM 
			ptl.NewsComment NewsComment
		INNER JOIN
			ptl.News News ON NewsComment.NewsID = News.ID
		INNER JOIN
			org.[User] CreatorComment ON NewsComment.UserID = CreatorComment.ID
		WHERE
			News.RemoverID IS NULL
		AND NewsComment.RemoverID IS NULL
		AND (@NewsID IS NULL OR NewsComment.NewsID = @NewsID)
		AND (@ParentID IS NULL OR NewsComment.ParentID = @ParentID)
		AND (@CommentType < 1 OR NewsComment.CommentType = @CommentType)
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