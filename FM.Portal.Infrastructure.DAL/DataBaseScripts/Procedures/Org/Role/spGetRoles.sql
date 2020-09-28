USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetRoles'))
	DROP PROCEDURE org.spGetRoles
GO

CREATE PROCEDURE org.spGetRoles
	@ApplicationID UNIQUEIDENTIFIER,
	@PositionID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
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
			Count(*) OVER() Total,
			rol.ID,
			rol.ApplicationID,
			rol.[Name]
		FROM org.[Role] rol
			LEFT JOIN org.PositionRole pRole ON pRole.RoleID = rol.ID  AND @PositionID IS NOT NULL
			LEFT JOIN org.Position pos ON pos.ID = pRole.PositionID AND pos.Id = @PositionID
			LEFT JOIN org.[User] usr ON usr.ID = pos.UserID AND usr.ID = @UserID
		WHERE rol.ApplicationID = @ApplicationID
			AND (@PositionID IS NULL OR pos.ID = @PositionID)
			AND (@UserID IS NULL OR usr.ID = @UserID)
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM
			MainSelect
	)
	SELECT *
	FROM MainSelect,TempCount
	ORDER BY [ID]
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE)
END