USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetUrlRecord'))
	DROP PROCEDURE pbl.spGetUrlRecord
GO

CREATE PROCEDURE pbl.spGetUrlRecord
@UrlDesc NVARCHAR(MAX),
@ID UNIQUEIDENTIFIER,
@EntityID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		*
	FROM pbl.UrlRecord
	WHERE
		(@UrlDesc IS NULL OR UrlDesc LIKE CONCAT('%',@UrlDesc,'%'))
	AND (@ID IS NULL OR ID  = @ID)
	AND (@EntityID IS NULL OR EntityID = @EntityID)
END