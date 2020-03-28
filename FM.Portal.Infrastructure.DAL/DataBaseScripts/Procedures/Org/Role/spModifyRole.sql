USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spModifyRole'))
	DROP PROCEDURE org.spModifyRole
GO

CREATE PROCEDURE org.spModifyRole
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ApplicationID UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@Permissions NVARCHAR(max)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;



	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1 -- insert
			BEGIN
				--SET @ID = COALESCE((SELECT MAX(ID) FROM org.[User]), 0) + 1

				INSERT INTO org.[Role]
				(ID, ApplicationID, [Name])
				VALUES
				(@ID, @ApplicationID, @Name)
			END
			ELSE
			BEGIN -- update
				UPDATE org.[Role]
				SET [Name] = @Name
				WHERE ID = @ID

				-- set permissions
			DELETE FROM org.RolePermission
			WHERE RoleID = @ID
			
			INSERT INTO org.RolePermission
			SELECT 
				NEWID() AS ID,
				@ID AS RoleID,
				splitdata AS CommandID 
			FROM 
				dbo.fnSplitString(@Permissions,',')

			END

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END