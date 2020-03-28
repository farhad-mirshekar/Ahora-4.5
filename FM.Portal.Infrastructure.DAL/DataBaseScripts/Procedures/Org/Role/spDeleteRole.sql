USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spDeleteRole'))
	DROP PROCEDURE org.spDeleteRole
GO

CREATE PROCEDURE org.spDeleteRole
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	IF EXISTS(SELECT TOP 1 1 FROM org.PositionRole WHERE RoleID = @ID)
		THROW 50000, N'این نقش توسط برخی از کاربران استفاده شده است. امکان حذف وجود ندارد.', 1
	
	BEGIN TRY
		BEGIN TRAN
			

			DELETE org.RolePermission
			WHERE RoleID = @ID

			DELETE org.[Role]
			WHERE ID = @ID

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH

	RETURN @@ROWCOUNT
END