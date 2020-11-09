USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsProductComment'))
	DROP PROCEDURE app.spGetsProductComment
GO

CREATE PROCEDURE app.spGetsProductComment
@ProductID UNIQUEIDENTIFIER,
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
			ProductComment.*
		FROM 
			app.ProductComment ProductComment
		INNER JOIN
			app.Product Product ON ProductComment.ProductID = Product.ID
		INNER JOIN
			org.[User] CreatorComment ON ProductComment.UserID = CreatorComment.ID
		WHERE
			Product.RemoverID IS NULL
		AND ProductComment.RemoverID IS NULL
		AND (@ProductID IS NULL OR ProductComment.ProductID = @ProductID)
		AND (@ParentID IS NULL OR ProductComment.ParentID = @ParentID)
		AND (@CommentType < 1 OR ProductComment.CommentType = @CommentType)
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