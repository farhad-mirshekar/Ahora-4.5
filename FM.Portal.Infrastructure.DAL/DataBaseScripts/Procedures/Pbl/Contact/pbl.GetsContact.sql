USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsContact'))
	DROP PROCEDURE pbl.spGetsContact
GO
CREATE PROCEDURE [pbl].[spGetsContact]
@Phone NVARCHAR(20),
@Email NVARCHAR(100),
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
			pbl.Contact Contact
		WHERE 
			(@Phone IS NULL OR Contact.Phone LIKE CONCAT('%' , @Phone , '%'))
		AND (@Email IS NULL OR Contact.Email LIKE CONCAT('%' , @Email , '%'))
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


