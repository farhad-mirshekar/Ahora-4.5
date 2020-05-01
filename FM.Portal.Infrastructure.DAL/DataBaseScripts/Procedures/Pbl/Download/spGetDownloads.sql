USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetDownloads'))
	DROP PROCEDURE pbl.spGetDownloads
GO

CREATE PROCEDURE pbl.spGetDownloads
	@PaymentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	SELECT 
		download.*
	FROM pbl.Download download
	WHERE 
		download.PaymentID = @PaymentID

	RETURN @@ROWCOUNT
END