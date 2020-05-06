USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spDeleteDepartment'))
	DROP PROCEDURE org.spDeleteDepartment
GO

CREATE PROCEDURE org.spDeleteDepartment
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE 
		@Node HIERARCHYID

	SET @Node = (SELECT [Node] FROM org.Department WHERE ID = @ID)  

	BEGIN TRY
		BEGIN TRAN

			DELETE FROM org.Department 
			WHERE [Node].IsDescendantOf(@Node) = 1

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	--RETURN @@ROWCOUNT
END