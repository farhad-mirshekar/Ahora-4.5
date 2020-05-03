USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetDownload'))
	DROP PROCEDURE pbl.spGetDownload
GO

CREATE PROCEDURE pbl.spGetDownload
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	--DECLARE @Expire SMALLDATETIME;
	--SET @Expire = (SELECT TOP 1 [ExpireDate] FROM pbl.Download WHERE ID = @ID)

	--IF @Expire < GETDATE()
	--	THROW 50000 , N'زمان توکن شما به پایان رسیده است',1
	SELECT 
		*
	FROM pbl.download	
	WHERE ID = @ID

	RETURN @@ROWCOUNT
END