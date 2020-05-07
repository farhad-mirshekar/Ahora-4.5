USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsUser'))
	DROP PROCEDURE org.spGetsUser
GO
CREATE PROCEDURE org.spGetsUser
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		*
	FROM [org].[User]
	WHERE
		[Enabled] = 1
		AND UserType = 1
END