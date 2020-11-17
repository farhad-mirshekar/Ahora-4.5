USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyUrlRecord'))
	DROP PROCEDURE pbl.spModifyUrlRecord
GO

CREATE PROCEDURE pbl.spModifyUrlRecord
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@EntityID UNIQUEIDENTIFIER,
	@EntityName NVARCHAR(400),
	@UrlDesc NVARCHAR(MAX),
	@Enabled TINYINT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.UrlRecord
			(ID,EntityID,EntityName,UrlDesc)
		VALUES
			(@ID,@EntityID,@EntityName,@UrlDesc)
	END
	ELSE
		UPDATE 
			pbl.UrlRecord
		SET
			[UrlDesc] = @UrlDesc
		WHERE
			EntityID = @EntityID
	RETURN @@ROWCOUNT
END