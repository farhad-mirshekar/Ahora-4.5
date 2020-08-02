﻿USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyBaseDocument'))
	DROP PROCEDURE pbl.spModifyBaseDocument
GO

CREATE PROCEDURE pbl.spModifyBaseDocument
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Type TINYINT,
	@PaymentID UNIQUEIDENTIFIER

--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN TRY
		BEGIN TRAN

		DECLARE @CurrentPositionID UNIQUEIDENTIFIER,
				@UserID UNIQUEIDENTIFIER

		SET @CurrentPositionID = (SELECT TOP 1 ID FROM org.Position WHERE [Type] = 100) -- rahbar
		SET @UserID = (SELECT TOP 1 UserID FROM org.Position WHERE [Type] = 100) -- rahbar
		INSERT INTO pbl.BaseDocument
			(ID,PaymentID,[Type],RemoverID , RemoveDate , CreationDate)
		VALUES
			(@ID,@PaymentID,@Type,NULL,NULL,GETDATE())

		INSERT INTO pbl.DocumentFlow
			(ID, DocumentID, [Date], FromPositionID, FromUserID, FromDocState, ToPositionID, ToDocState, SendType, Comment)
		VALUES
			(NEWID(), @ID, GETDATE(), @CurrentPositionID, @UserID, 1, @CurrentPositionID, 1, 1, N'ثبت اولیه')
	COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	RETURN @@ROWCOUNT
END