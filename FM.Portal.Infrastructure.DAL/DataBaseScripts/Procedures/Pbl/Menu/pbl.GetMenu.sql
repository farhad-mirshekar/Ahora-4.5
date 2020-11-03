USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetMenu'))
	DROP PROCEDURE pbl.spGetMenu
GO

CREATE PROCEDURE pbl.spGetMenu
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT
		Menu.ID,
		Menu.LanguageID,
		Menu.[Name],
		Menu.UserID,
		Menu.CreationDate,
		Menu.RemoverDate,
		Menu.RemoverID,
		lng.[Name] AS LanguageName
	FROM 
		pbl.Menu Menu
	INNER JOIN pbl.[Language] lng ON Menu.LanguageID = lng.ID
	WHERE 
		(Menu.ID = @ID) 

	RETURN @@ROWCOUNT
END