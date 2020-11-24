USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spDeletePages'))
	DROP PROCEDURE ptl.spDeletePages
GO

CREATE PROCEDURE ptl.spDeletePages
	@ID UNIQUEIDENTIFIER,
	@PageType TINYINT
--WITH ENCRYPTION
AS
BEGIN
	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRAN
			IF @PageType = 1
				BEGIN
					DELETE FROM ptl.DynamicPage WHERE PageID = @ID
				END
			IF @PageType = 2
				BEGIN
					DELETE FROM ptl.StaticPage WHERE ID = @ID
				END

		DELETE FROM ptl.Pages
		WHERE 
			ID = @ID
	COMMIT
		RETURN @@ROWCOUNT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH


END