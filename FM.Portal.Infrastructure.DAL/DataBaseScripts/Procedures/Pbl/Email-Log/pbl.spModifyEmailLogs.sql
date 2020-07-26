USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyEmailLogs'))
	DROP PROCEDURE pbl.spModifyEmailLogs
GO

CREATE PROCEDURE pbl.spModifyEmailLogs
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@From NVARCHAR(1000),
	@TO NVARCHAR(1000),
	@Message NVARCHAR(MAX),
	@IP NVARCHAR(15),
	@UserID UNIQUEIDENTIFIER,
	@EmailStatusType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.[EmailLogs]
			([ID],[From],[To],[Message],[IP],[UserID],[EmailStatusType],[CreationDate])
		VALUES
			(@ID,@From,@To,@Message,@IP,@UserID,@EmailStatusType,GETDATE())
	END
	RETURN @@ROWCOUNT
END