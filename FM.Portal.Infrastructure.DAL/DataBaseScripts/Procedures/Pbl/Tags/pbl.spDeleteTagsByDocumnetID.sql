USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteTagsByDocumentID'))
	DROP PROCEDURE pbl.spDeleteTagsByDocumentID
GO

CREATE PROCEDURE pbl.spDeleteTagsByDocumentID
	@DocumentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM pbl.Tags_Mapping WHERE DocumentID = @DocumentID
	RETURN @@ROWCOUNT
END