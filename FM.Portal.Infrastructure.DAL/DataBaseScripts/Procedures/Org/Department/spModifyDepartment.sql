USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spModifyDepartment'))
	DROP PROCEDURE org.spModifyDepartment
GO

CREATE PROCEDURE org.spModifyDepartment
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@Node HIERARCHYID,
	@Name Nvarchar(256),
	@Address Nvarchar(1000),
	@PostalCode Char(10),
	@Phone NvarChar(11),
	@CodePhone NvarChar(3)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE 
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@NewNode HIERARCHYID

	IF EXISTS(SELECT 1 FROM org.Department WHERE ID <> @ID AND REPLACE([Name], ' ', '') = REPLACE(@Name, ' ', ''))
		THROW 50000, N'نام تکراری است', 1

	IF @Node IS NULL 
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM org.Department WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM org.Department WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM org.Department WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END

	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO org.Department
				(ID, [Node], [Name], [Address], [PostalCode], [Phone], [CodePhone],[Enabled])
				VALUES
				(@ID, @NewNode,@Name, @Address, @PostalCode, @Phone, @CodePhone,1)
			END
			ELSE
			BEGIN -- update
				UPDATE org.Department
				SET 
					[Name] = @Name, 
					[Address] = @Address,
					[PostalCode] = @PostalCode,
					[Phone] = @Phone,
					[CodePhone] = @CodePhone
				WHERE ID = @ID

				IF(@Node <> @NewNode)
				BEGIN
					UPDATE org.Department
					SET [Node] = [Node].GetReparentedValue(@Node, @NewNode)
					WHERE [Node].IsDescendantOf(@Node) = 1
				END
			END

		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END