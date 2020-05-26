USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyContact'))
	DROP PROCEDURE pbl.spModifyContact
GO

CREATE PROCEDURE pbl.spModifyContact
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(1000),
	@LastName NVARCHAR(1000),
	@Title NVARCHAR(1000),
	@Description NVARCHAR(MAX),
	@Phone NVARCHAR(20),
	@Email NVARCHAR(100)
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1
	BEGIN
		INSERT INTO pbl.Contact
			(ID,FirstName,LastName,Title,[Description],Phone,Email,CreationDate)
		VALUES
			(@ID,@FirstName,@LastName,@Title,@Description,@Phone,@Email,GETDATE())
	END
	RETURN @@ROWCOUNT
END