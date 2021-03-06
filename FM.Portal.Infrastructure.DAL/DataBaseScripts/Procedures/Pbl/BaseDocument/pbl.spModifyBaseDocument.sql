﻿USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyBaseDocument'))
	DROP PROCEDURE pbl.spModifyBaseDocument
GO

CREATE PROCEDURE pbl.spModifyBaseDocument
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER

--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN TRY
		BEGIN TRAN

		DECLARE @CurrentPositionID UNIQUEIDENTIFIER,
				@UserID UNIQUEIDENTIFIER,
				@ToPositionID UNIQUEIDENTIFIER

		SET @CurrentPositionID = (SELECT TOP 1 ID FROM org.Position WHERE [Type] = 100) -- rahbar
		SET @UserID = (SELECT TOP 1 UserID FROM org.Position WHERE [Type] = 100) -- rahbar
		SET @ToPositionID = (
		SELECT TOP 1 ID FROM org.Position WHERE DepartmentID IN 
			(SELECT ID FROM org.Department WHERE [Type] = 3) )

		INSERT INTO pbl.BaseDocument
			(ID,RemoverID , RemoveDate , CreationDate)
		VALUES
			(@ID,NULL,NULL,GETDATE())

		INSERT INTO pbl.DocumentFlow
			(ID, DocumentID, [Date], FromPositionID, FromUserID, FromDocState, ToPositionID, ToDocState, SendType, Comment,ReadDate,ActionDate)
		VALUES
			(NEWID(), @ID, GETDATE(), @CurrentPositionID, @UserID, 1, @CurrentPositionID, 1, 1, N'ثبت اولیه',GETDATE(),GETDATE())

		IF @ToPositionID IS NOT NULL
		BEGIN
		INSERT INTO pbl.DocumentFlow
			(ID, DocumentID, [Date], FromPositionID, FromUserID, FromDocState, ToPositionID, ToDocState, SendType, Comment)
		VALUES
			(NEWID(), @ID, GETDATE(), @CurrentPositionID, @UserID, 1, @ToPositionID, 10, 1, N'ارجاع به واحد مالی')
		END
	COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	RETURN @@ROWCOUNT
END