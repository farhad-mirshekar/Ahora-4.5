USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsDepartmentByNode'))
	DROP PROCEDURE org.spGetsDepartmentByNode
GO

CREATE PROCEDURE org.spGetsDepartmentByNode
	@Node HIERARCHYID

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;



	BEGIN TRY
		BEGIN TRAN
			SELECT 
			 Department.ID,
			Department.[Node].ToString() Node,
			Department.[Node].GetAncestor(1).ToString() ParentNode,
			Department.[Name],
			Department.[Address],
			Department.PostalCode,
			Department.Phone,
			Department.CodePhone
			FROM org.Department
			WHERE Node = @Node
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END