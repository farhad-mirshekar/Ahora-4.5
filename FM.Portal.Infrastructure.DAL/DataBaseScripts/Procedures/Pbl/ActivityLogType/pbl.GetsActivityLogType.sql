USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsActivityLogType'))
	DROP PROCEDURE pbl.spGetsActivityLogType
GO
CREATE PROCEDURE [pbl].spGetsActivityLogType
@SystemKeyword NVARCHAR(2000),
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
			*
		FROM 
			pbl.ActivityLogType ActivityLogType
		WHERE 
			(@SystemKeyword IS NULL OR ActivityLogType.SystemKeyword LIKE CONCAT('%' , @SystemKeyword , '%'))
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


