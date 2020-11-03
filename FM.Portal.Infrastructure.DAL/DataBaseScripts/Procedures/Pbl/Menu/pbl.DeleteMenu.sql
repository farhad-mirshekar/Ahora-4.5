USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteMenu'))
	DROP PROCEDURE pbl.spDeleteMenu
GO

CREATE PROCEDURE pbl.spDeleteMenu
	@ID UNIQUEIDENTIFIER,
	@RemoverID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	UPDATE pbl.Menu
	SET 
		RemoverID = @RemoverID,
		RemoverDate = GETDATE()
	WHERE
		(ID = @ID)
		
	RETURN @@ROWCOUNT
END