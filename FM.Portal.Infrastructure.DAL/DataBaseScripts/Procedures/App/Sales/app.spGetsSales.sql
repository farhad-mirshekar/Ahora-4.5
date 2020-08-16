USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsSales'))
	DROP PROCEDURE app.spGetsSales
GO

CREATE PROCEDURE app.spGetsSales
@UserPositionID UNIQUEIDENTIFIER,
@ActionState TINYINT
--WITH ENCRYPTION
AS
BEGIN
	;WITH Flow AS
	(
		SELECT 
			DISTINCT
			DocumentID
		FROM
			pbl.DocumentFlow 
		WHERE
			ToPositionID = @UserPositionID 
	)
	SELECT 
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
END