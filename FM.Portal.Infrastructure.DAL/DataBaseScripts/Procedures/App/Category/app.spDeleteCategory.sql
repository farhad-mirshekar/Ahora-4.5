USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('app.spDeleteCategory'))
	DROP PROCEDURE app.spDeleteCategory
GO

CREATE PROCEDURE app.spDeleteCategory
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE 
		@Node HIERARCHYID

	SET @Node = (SELECT [Node] FROM app.Category WHERE ID = @ID)  

	BEGIN TRY
		BEGIN TRAN
			DELETE FROM app.Category_Discount_Mapping WHERE CategoryID = @ID
			DELETE FROM app.Category 
			WHERE [Node].IsDescendantOf(@Node) = 1
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	--RETURN @@ROWCOUNT
END