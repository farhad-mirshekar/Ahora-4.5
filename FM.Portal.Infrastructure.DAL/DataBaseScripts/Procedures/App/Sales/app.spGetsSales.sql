USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsSales'))
	DROP PROCEDURE app.spGetsSales
GO

CREATE PROCEDURE app.spGetsSales
@UserPositionID UNIQUEIDENTIFIER,
@ActionState TINYINT,
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
	;WITH Flow AS
	(
		SELECT 
			DISTINCT
			DocumentID
		FROM
			pbl.DocumentFlow 
		WHERE
			ToPositionID = @UserPositionID 
	),MainSelect AS
	(
		SELECT 
			DISTINCT
			Sales.*,
			lastFromUser.FirstName + ' ' + lastFromUser.LastName LastFromUserName,
			lastToUser.FirstName + ' ' + lastToUser.LastName LastToUserName,
			lastToPosition.[Type] LastToPositionType,
			lastFromPosition.[Type] LastFromPositionType
		FROM app.vwSales Sales
		LEFT JOIN org.[User] lastFromUser ON lastFromUser.ID = Sales.lastFromUserID
		LEFT JOIN org.Position lastToPosition ON lastToPosition.ID = Sales.LastToPositionID
		LEFT JOIN org.[User] lastToUser ON lastToUser.ID = lastToPosition.UserID
		LEFT JOIN org.Position lastFromPosition ON lastFromPosition.ID = Sales.LastFromPositionID
		INNER JOIN Flow flow ON flow.DocumentID = Sales.ID
		WHERE
			@ActionState IN (1,2,3)
		AND (@ActionState <> 1 OR Sales.LastToPositionID = @UserPositionID)
		AND (@ActionState <> 2 OR Sales.LastToPositionID <> @UserPositionID)
		AND (@ActionState <> 3 OR Sales.LastDocState = 100)
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)
	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [CreationDate] DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END