IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spSetFlowReadState'))
	DROP PROCEDURE pbl.spSetFlowReadState
GO

CREATE PROCEDURE pbl.spSetFlowReadState
	@DocumentID UNIQUEIDENTIFIER
	, @UserPositionID UNIQUEIDENTIFIER
	, @IsRead BIT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE  @FlowID UNIQUEIDENTIFIER
			, @ToPositionID UNIQUEIDENTIFIER

	IF @DocumentID IS NULL
		RETURN -2  -- وضعیت تایید مشخص نشده است

	SET @FlowID = (SELECT ID FROM pbl.DocumentFlow WHERE DocumentID = @DocumentID AND ActionDate IS NULL)
	SET @ToPositionID = (SELECT ToPositionID FROM pbl.DocumentFlow WHERE ID = @FlowID)

	IF @ToPositionID <> @UserPositionID
		RETURN -3

	BEGIN TRY
		BEGIN TRAN
			
			UPDATE pbl.DocumentFlow
			SET ReadDate = case 
							when @IsRead = 1 
								then GETDATE() 
							when @IsRead = 0 
								then null end
			WHERE ID = @FlowID
		COMMIT

	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

END