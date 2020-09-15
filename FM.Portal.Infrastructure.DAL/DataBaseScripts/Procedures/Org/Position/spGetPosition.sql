USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spGetPosition'))
	DROP PROCEDURE org.spGetPosition
GO

CREATE PROCEDURE org.spGetPosition
	@ID UNIQUEIDENTIFIER
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CurrentDate SMALLDATETIME = GETDATE()
	
	SELECT p.ID
		, p.ApplicationID
		, p.DepartmentID
		, d.[Name] DepartmentName
		, d.[Type] DepartmentType
		, p.UserID
		, u.Username
		, u.FirstName
		, u.LastName
		, u.NationalCode
		, u.Email
		, u.EmailVerified
		, u.CellPhone
		, u.CellPhoneVerified
		, u.[Enabled] UserEnabled
		, CAST(CASE WHEN @CurrentDate > u.PasswordExpireDate THEN 1 ELSE 0 END AS BIT) PasswordExpired
		, p.[Type]
		, p.[Default]
		, p.[Enabled]
		, parent.ID ParentID
		, p.[Node].ToString() Node
		, p.[Node].GetAncestor(1).ToString() ParentNode
	FROM org.Position p
		INNER JOIN org.Department d ON p.DepartmentID = d.id
		LEFT JOIN org.[User] u ON p.UserID = u.ID
		LEFT JOIN org.Position parent ON p.Node.GetAncestor(1) = parent.Node AND parent.ApplicationID = p.ApplicationID
	where (p.RemoverID IS NULL AND parent.RemoverID IS NULL)
		AND p.ID = @ID

	RETURN @@ROWCOUNT
END