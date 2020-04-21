USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyPayment'))
	DROP PROCEDURE app.spModifyPayment
GO

CREATE PROCEDURE app.spModifyPayment
@ID UNIQUEIDENTIFIER,
@IsNewRecord BIT,
@RetrivalRefNo NVARCHAR(MAX),
@SystemTraceNo NVARCHAR(MAX),
@TransactionStatusMessage NVARCHAR(MAX),
@TransactionStatus INT
--WITH ENCRYPTION
AS
BEGIN
	UPDATE
		app.[Payment]
	SET
		RetrivalRefNo = LTRIM(RTRIM(@RetrivalRefNo)),
		SystemTraceNo = LTRIM(RTRIM(@SystemTraceNo)),
		TransactionStatusMessage = LTRIM(RTRIM(@TransactionStatusMessage)),
		TransactionStatus = @TransactionStatus
	WHERE
		ID = @ID
	RETURN @@ROWCOUNT
END