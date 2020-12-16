USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spGetsEventsComment'))
	DROP PROCEDURE ptl.spGetsEventsComment
GO

CREATE PROCEDURE ptl.spGetsEventsComment
@EventsID UNIQUEIDENTIFIER,
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
			EventsComment.*,
			CONCAT(CreatorComment.FirstName , ' ' , CreatorComment.LastName) AS CreatorName,
			[Events].Title As EventsTitle
		FROM 
			ptl.EventsComment EventsComment
		INNER JOIN
			ptl.[Events] [Events] ON EventsComment.EventsID = Events.ID
		INNER JOIN
			org.[User] CreatorComment ON EventsComment.UserID = CreatorComment.ID
		WHERE
			[Events].RemoverID IS NULL
		AND EventsComment.RemoverID IS NULL
		AND (@EventsID IS NULL OR EventsComment.EventsID = @EventsID)
		AND (@ParentID IS NULL OR EventsComment.ParentID = @ParentID)
		AND (@CommentType < 1 OR EventsComment.CommentType = @CommentType)
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