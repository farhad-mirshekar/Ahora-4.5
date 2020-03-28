USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetCommandByNode'))
	DROP PROCEDURE org.spGetCommandByNode
GO

CREATE PROCEDURE org.spGetCommandByNode
	@Node HIERARCHYID

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;



	BEGIN TRY
		BEGIN TRAN
			SELECT 
			 Command.ID,
			Command.ApplicationID,
			Command.[Node].ToString() Node,
			Command.[Node].GetAncestor(1).ToString() ParentNode,
			Command.[Name],
			Command.FullName,
			Command.Title,
			Command.[Type]
			FROM org.Command
			WHERE Node = @Node
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END