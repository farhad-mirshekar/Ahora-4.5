USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsActivityLog'))
	DROP PROCEDURE pbl.spGetsActivityLog
GO
CREATE PROCEDURE [pbl].spGetsActivityLog
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
			ActivityLog.*,
			CONCAT(CreatorUser.FirstName ,' ' , CreatorUser.LastName) AS CreatorName
		FROM 
			pbl.ActivityLog ActivityLog
		INNER JOIN
			org.[User] CreatorUser ON ActivityLog.UserID = CreatorUser.ID
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
GO


