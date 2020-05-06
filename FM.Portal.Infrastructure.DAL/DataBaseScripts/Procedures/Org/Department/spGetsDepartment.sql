USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetsDepartment'))
	DROP PROCEDURE org.spGetsDepartment
GO

CREATE PROCEDURE org.spGetsDepartment

--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		department.ID,
		department.[Node].ToString() Node,
		department.[Node].GetAncestor(1).ToString() ParentNode,
		department.[Name],
		department.[Address],
		department.PostalCode,
		department.Phone,
		department.CodePhone
	FROM org.Department department
	ORDER BY [Name]

	RETURN @@ROWCOUNT
END