USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetTagsByDocumentID'))
	DROP PROCEDURE pbl.spGetTagsByDocumentID
GO

CREATE PROCEDURE pbl.spGetTagsByDocumentID
	@DocumentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		tag.[Name]
	FROM 
		pbl.Tags tag
	INNER JOIN
		pbl.Tags_Mapping map ON tag.ID = map.TagID
	WHERE
		map.DocumentID = @DocumentID
END