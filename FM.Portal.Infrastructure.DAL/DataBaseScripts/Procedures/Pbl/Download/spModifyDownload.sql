USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyDownload'))
	DROP PROCEDURE pbl.spModifyDownload
GO

CREATE PROCEDURE pbl.spModifyDownload
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@Comment NVARCHAR(256),
	@Data VARBINARY(MAX),
	@PaymentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF DATALENGTH(@Data) < 1
		THROW 50000, N'فایل بارگذاری نشده است', 1

	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO pbl.Download
				(ID, [UserID],[Comment],[Data],[CreationDate],[ExpireDate],[PaymentID])
				VALUES
				(@ID, @UserID, @Comment, @Data, GETDATE(),DATEADD(DAY , 1 , GETDATE()) , @PaymentID)
			END
			ELSE
			BEGIN
				UPDATE pbl.Download
				SET 
					CreationDate = GETDATE(),
					[ExpireDate] = DATEADD(DAY , 1 , GETDATE())
				WHERE ID = @ID
			END
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END