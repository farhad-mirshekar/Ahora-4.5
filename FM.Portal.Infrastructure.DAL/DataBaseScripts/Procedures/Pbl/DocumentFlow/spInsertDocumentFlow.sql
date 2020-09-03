USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spInsertDocumentFlow'))
	DROP PROCEDURE pbl.spInsertDocumentFlow
GO

CREATE PROCEDURE pbl.spInsertDocumentFlow
	@Comment NVARCHAR(4000),
	@DocumentID UNIQUEIDENTIFIER,
	@FromDocState TINYINT,
	@FromPositionID UNIQUEIDENTIFIER,
	@ToDocState TINYINT,
	@ToPositionID UNIQUEIDENTIFIER,
	@SendType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	DECLARE
		@ID UNIQUEIDENTIFIER,
		@Date DATETIME,
		@TmpFromUserID UNIQUEIDENTIFIER,
		@LastFlowID UNIQUEIDENTIFIER,
		@DocumentType TINYINT,
		@FromUserID UNIQUEIDENTIFIER,
		@LastFromPositionID UNIQUEIDENTIFIER,
		@LastToPositionID UNIQUEIDENTIFIER,
		@LastFromDocState TINYINT,
		@LastToDocState TINYINT

	IF @Comment = '' SET @Comment = NULL
	SET @LastFlowID = (SELECT TOP(1) ID FROM pbl.DocumentFlow WHERE DocumentID = @DocumentID AND ActionDate IS NULL ORDER BY [Date] DESC)
	SET @FromDocState = COALESCE(@FromDocState, (SELECT TOP 1 ToDocState FROM pbl.DocumentFlow WHERE DocumentID = @DocumentID ORDER BY DATE DESC))
	SET @FromUserID = (SELECT UserID FROM org.position WHERE ID = @FromPositionID)
	
	IF @FromUserID IS NULL SET @FromUserID = CAST(0x0 AS UNIQUEIDENTIFIER)
	
	SELECT @LastFromPositionID = FromPositionID,
		@LastToPositionID = ToPositionID,
		@LastFromDocState = FromDocState,
		@LastToDocState = ToDocState
	FROM pbl.DocumentFlow WHERE ID = @LastFlowID

	IF @LastFromPositionID = @FromPositionID
		AND @LastToPositionID = @ToPositionID
		AND @LastFromDocState = @FromDocState
		AND @LastToDocState = @ToDocState
		THROW 50000, N'خطا در ارسال', 1

	BEGIN TRY
		BEGIN TRAN
			
			SET @ID  = NEWID()
			SET @Date = GETDATE()

			INSERT INTO pbl.DocumentFlow
			(ID, DocumentID, [Date], FromPositionID, FromUserID, FromDocState, ToPositionID, ToDocState, SendType, Comment)
			VALUES
			(@ID, @DocumentID, @Date, @FromPositionID, @FromUserID, @FromDocState, @ToPositionID, @ToDocState, @SendType, @Comment)

			-- set action date for last flow
			UPDATE pbl.DocumentFlow
			SET ActionDate = @Date
			WHERE ID = @LastFlowID 
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	RETURN @@ROWCOUNT
END