USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyActivityLog'))
	DROP PROCEDURE pbl.spModifyActivityLog
GO

CREATE PROCEDURE pbl.spModifyActivityLog
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ActivityLogTypeID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@Comment NVARCHAR(MAX),
	@IpAddress NVARCHAR(200),
	@EntityName NVARCHAR(MAX),
	@EntityID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.ActivityLog
			(ID,ActivityLogTypeID,Comment,EntityID,EntityName,IpAddress,UserID)
		VALUES
			(@ID,@ActivityLogTypeID,@Comment,@EntityID,@EntityName,@IpAddress,@UserID)
	END
	RETURN @@ROWCOUNT
END