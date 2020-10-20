USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsUserAddress'))
	DROP PROCEDURE org.spGetsUserAddress
GO

CREATE PROCEDURE org.spGetsUserAddress
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

	;WITH MainSelect AS (
	SELECT 
		useraddress.*,
		users.CellPhone
	FROM
		org.UserAddress useraddress
	INNER JOIN
		org.[User] users
	ON
		useraddress.UserID = users.ID
	WHERE
		users.ID = @UserID AND
		users.Enabled = 1
	), TempCount AS 
	(
		SELECT COUNT(*) AS Total FROM MainSelect
	)

	SELECT * FROM MainSelect, TempCount						
	ORDER BY CreationDate DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION (RECOMPILE);
END