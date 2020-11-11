USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyActivityLogType'))
	DROP PROCEDURE pbl.spModifyActivityLogType
GO

CREATE PROCEDURE pbl.spModifyActivityLogType
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@SystemKeyword NVARCHAR(2000),
	@Name NVARCHAR(2000),
	@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.ActivityLogType
			(ID,SystemKeyword , [Name] , [Enabled])
		VALUES
			(@ID,@SystemKeyword , @Name , @Enabled)
	END
	ELSE
		UPDATE pbl.ActivityLogType
		SET
			[SystemKeyword] = @SystemKeyword,
			[Name] = @Name, 
			[Enabled] = @Enabled
		WHERE
			ID= @ID
	RETURN @@ROWCOUNT
END