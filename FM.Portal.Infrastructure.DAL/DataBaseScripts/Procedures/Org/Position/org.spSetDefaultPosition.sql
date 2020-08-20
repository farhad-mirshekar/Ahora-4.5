USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spSetDefaultPosition'))
	DROP PROCEDURE org.spSetDefaultPosition
GO
CREATE PROCEDURE org.spSetDefaultPosition
@ID UNIQUEIDENTIFIER

--WITH ENCRYPTION
AS
BEGIN
	SET XACT_ABORT ON;

	DECLARE @UserID UNIQUEIDENTIFIER,
			@ApplicationID UNIQUEIDENTIFIER

	SELECT
		@UserID = UserID,
		@ApplicationID = ApplicationID
	FROM 
		org.Position
	WHERE
		ID = @ID

	BEGIN TRY
		BEGIN TRAN

			UPDATE org.Position
			SET [Default] = 0
			WHERE 
				UserID = @UserID
			AND ApplicationID = @ApplicationID

			UPDATE org.Position
			SET [Default] = 1
			WHERE ID = @ID

		COMMIT
		END TRY
		BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT

END