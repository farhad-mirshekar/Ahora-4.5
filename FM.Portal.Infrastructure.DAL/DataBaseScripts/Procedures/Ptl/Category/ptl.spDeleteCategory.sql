USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spDeleteCategory'))
	DROP PROCEDURE ptl.spDeleteCategory
GO

CREATE PROCEDURE ptl.spDeleteCategory
	@ID UNIQUEIDENTIFIER,
	@RemoverID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE 
		@Node HIERARCHYID

	SET @Node = (SELECT [Node] FROM ptl.Category WHERE ID = @ID)  

	BEGIN TRY
		BEGIN TRAN
			UPDATE ptl.Category
			SET
				RemoverID = @RemoverID,
				RemoverDate = GETDATE()
			WHERE [Node].IsDescendantOf(@Node) = 1
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	--RETURN @@ROWCOUNT
END