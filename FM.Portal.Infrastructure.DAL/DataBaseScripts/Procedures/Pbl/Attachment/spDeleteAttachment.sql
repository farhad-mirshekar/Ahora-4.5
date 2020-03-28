USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteAttachment'))
	DROP PROCEDURE pbl.spDeleteAttachment
GO

CREATE PROCEDURE pbl.spDeleteAttachment
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;


				
	---- Begin Validation
	IF @ID IS NULL
		RETURN -2 -- شناسه اپلیکیشن نامعتبر است
	---- End Validation

	BEGIN TRY
		BEGIN TRAN
			DELETE FROM pbl.Attachment
			WHERE ID = @ID
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END