USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetPage'))
	DROP PROCEDURE ptl.spGetPage
GO

CREATE PROCEDURE ptl.spGetPage
	@ID UNIQUEIDENTIFIER,
	@TrackingCode NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		pages.*
	FROM ptl.Pages pages
	WHERE 
		(@ID IS NULL OR ID = @ID)
	AND (@TrackingCode IS NULL OR TrackingCode = @TrackingCode)
	RETURN @@ROWCOUNT
END