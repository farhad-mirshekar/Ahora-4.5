USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetDepartment'))
	DROP PROCEDURE org.spGetDepartment
GO

CREATE PROCEDURE org.spGetDepartment
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;


	SELECT 
		ID,
		Node.ToString() Node,
		Node.GetAncestor(1).ToString() ParentNode,
		[Name],
		[Address],
		PostalCode,
		Phone,
		CodePhone,
		RemoverDate,
		RemoverID
	FROM org.Department
	WHERE (ID = @ID)

	RETURN @@ROWCOUNT
END