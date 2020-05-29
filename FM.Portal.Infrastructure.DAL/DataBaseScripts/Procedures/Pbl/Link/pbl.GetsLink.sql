USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsLink'))
	DROP PROCEDURE pbl.spGetsLink
GO

CREATE PROCEDURE pbl.spGetsLink
@ShowFooter BIT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		link.*
	FROM pbl.Link link
	WHERE
		(@ShowFooter IS NULL OR link.ShowFooter = @ShowFooter)
	ORDER BY [Priority],[CreationDate]

	RETURN @@ROWCOUNT
END