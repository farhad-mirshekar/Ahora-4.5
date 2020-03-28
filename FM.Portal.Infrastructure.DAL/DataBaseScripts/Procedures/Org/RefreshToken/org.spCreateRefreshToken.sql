USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spCreateRefreshToken'))
	DROP PROCEDURE org.spCreateRefreshToken
GO

CREATE PROCEDURE org.spCreateRefreshToken
	@ID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@IssuedDate DATETIME,
	@ExpireDate DATETIME,
	@ProtectedTicket NVARCHAR(MAX)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRAN
			
			DELETE org.RefreshToken 
			WHERE UserID = @UserID

			SET @ProtectedTicket = LTRIM(RTRIM(@ProtectedTicket))

			INSERT INTO org.RefreshToken
				(ID, UserID, IssuedDate, [ExpireDate], ProtectedTicket)
			VALUES
				(@ID, @UserID, @IssuedDate, @ExpireDate, @ProtectedTicket)

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END