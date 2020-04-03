USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyTags'))
	DROP PROCEDURE pbl.spModifyTags
GO

CREATE PROCEDURE pbl.spModifyTags
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@DocumentID UNIQUEIDENTIFIER,
	@Name NVARCHAR(MAX)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRAN
	IF @IsNewRecord = 1 -- insert
			BEGIN
				DECLARE @IDs UNIQUEIDENTIFIER;
				SET @IDs = (SELECT ID FROM pbl.Tags tag WHERE tag.[Name] LIKE N'%'+@Name+'%')
				IF @IDs IS NULL
				BEGIN
					INSERT INTO pbl.Tags
					(ID, [Name], [CreationDate])
					VALUES
					(@ID, @Name, GETDATE())

					EXEC pbl.spModifyTagsMapping @ID,@DocumentID
				END
				ELSE
				BEGIN
				EXEC pbl.spModifyTagsMapping @IDs,@DocumentID
					UPDATE pbl.Tags
					SET 
						[Name] = @Name
					WHERE ID = @ID
				END
			END
	COMMIT
	RETURN @@ROWCOUNT
END