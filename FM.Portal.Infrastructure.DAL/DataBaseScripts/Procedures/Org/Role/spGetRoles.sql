USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetRoles'))
	DROP PROCEDURE org.spGetRoles
GO

CREATE PROCEDURE org.spGetRoles
	@ApplicationID UNIQUEIDENTIFIER,
	@PositionID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN

	SET NOCOUNT ON;

	;WITH MainSelect AS (
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
	)
	SELECT * FROM MainSelect		 
	ORDER BY [ID]

	RETURN @@ROWCOUNT
END