USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyLink'))
	DROP PROCEDURE pbl.spModifyLink
GO

CREATE PROCEDURE pbl.spModifyLink
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@Url Nvarchar(max),
	@IconText Nvarchar(100),
	@ShowFooter Bit,
	@Description Nvarchar(1000),
	@Enabled Tinyint,
	@Priority int,
	@Name Nvarchar(1000)
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		IF @Priority = 0
		BEGIN
			SET @Priority = (SELECT TOP 1 [Priority] FROM pbl.Link ORDER BY CreationDate DESC)
		END
		INSERT INTO pbl.Link (ID,UserID,[Enabled],Url,[Description],IconText,ShowFooter,[Priority],CreationDate,[Name])
		VALUES
		(@ID,@UserID,@Enabled,@Url,@Description,@IconText,@ShowFooter,@Priority,GETDATE(),@Name)
	END
	ELSE
	BEGIN
		UPDATE pbl.Link
		SET
			[Enabled] = @Enabled,
			Url = @Url,
			[Description] = @Description,
			IconText = @IconText,
			ShowFooter = @ShowFooter,
			[Priority] = @Priority,
			[Name] = @Name
		WHERE
			ID = @ID
	END
	RETURN @@ROWCOUNT
END