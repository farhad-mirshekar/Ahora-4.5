USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spDeleteCommand'))
	DROP PROCEDURE org.spDeleteCommand
GO

CREATE PROCEDURE org.spDeleteCommand
	@ID UNIQUEIDENTIFIER,
	@ApplicationID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE 
		@Node HIERARCHYID

	SET @Node = (SELECT [Node] FROM org.Command WHERE ID = @ID)  

	BEGIN TRY
		BEGIN TRAN

			DELETE FROM org.RolePermission 
			WHERE CommandID = @ID

			DELETE FROM org.Command 
			WHERE [Node].IsDescendantOf(@Node) = 1
			AND ApplicationID = @ApplicationID

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	--RETURN @@ROWCOUNT
END