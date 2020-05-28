USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyAttachment'))
	DROP PROCEDURE pbl.spModifyAttachment
GO

CREATE PROCEDURE pbl.spModifyAttachment
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@Type TINYINT,
	@FileName NVARCHAR(256),
	@Comment NVARCHAR(256),
	@Data VARBINARY(MAX),
	@PathType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF @ParentID IS NULL
		THROW 50000, N'شناسه پدر مشخص نشده است', 1

	IF @FileName IS NULL OR @FileName = ''
		THROW 50000, N'نام فایل مشخص نشده است', 1

	IF DATALENGTH(@Data) < 1
		THROW 50000, N'فایل بارگذاری نشده است', 1

	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO pbl.AttachmentExtra
				(ID,ParentID ,[Data],CreationDate)
				VALUES
				(@ID,@ParentID , @Data , GETDATE())

				INSERT INTO pbl.Attachment
				(ID, ParentID, [Type], [FileName], [CreationDate],[PathType])
				VALUES
				(@ID, @ParentID, @Type, @FileName, GETDATE(),@PathType)
			END
			ELSE
			BEGIN
				SET @ParentID = (SELECT ParentID FROM pbl.Attachment WHERE ID = @ID)

				UPDATE pbl.AttachmentExtra
				SET [Data] = @Data
				WHERE ID = @ID

				UPDATE pbl.Attachment
				SET [FileName] = @FileName
				WHERE ID = @ID
			END
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END