USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetBaseDocument'))
	DROP PROCEDURE pbl.spGetBaseDocument
GO

CREATE PROCEDURE pbl.spGetBaseDocument
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		*
	FROM pbl.BaseDocument
	WHERE ID = @ID

	RETURN @@ROWCOUNT
END