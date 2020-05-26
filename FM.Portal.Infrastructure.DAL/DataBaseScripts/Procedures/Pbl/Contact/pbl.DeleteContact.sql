USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteContact'))
	DROP PROCEDURE pbl.spDeleteContact
GO

CREATE PROCEDURE pbl.spDeleteContact
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	DELETE FROM pbl.Contact WHERE ID = @ID
END