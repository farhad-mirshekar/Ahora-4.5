USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyCategory'))
	DROP PROCEDURE ptl.spModifyCategory
GO

CREATE PROCEDURE ptl.spModifyCategory
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(50),
@ParentID UNIQUEIDENTIFIER,
@IsNewRecord BIT,
@Node HIERARCHYID
--WITH ENCRYPTION
AS
BEGIN
	SET XACT_ABORT ON;

	DECLARE 
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@NewNode HIERARCHYID

	IF EXISTS(SELECT 1 FROM ptl.Category WHERE ID <> @ID AND REPLACE([Title], ' ', '') = REPLACE(@Title, ' ', ''))
		THROW 50000, N'نام تکراری است', 1

	IF @Node IS NULL 
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM ptl.Category WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM ptl.Category WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM ptl.Category WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END
	BEGIN TRY
		BEGIN TRAN
			IF @IsNewRecord = 1 --insert
				BEGIN
					INSERT INTO [ptl].[Category]
						(ID,[Title],[Node], CreationDate)
					VALUES
						(@ID, @Title , @NewNode , GETDATE())
				END
			ELSE -- update
				BEGIN
					UPDATE [ptl].[Category]
					SET
						[Title] = @Title
					WHERE
						[ID] = @ID

						IF(@Node <> @NewNode)
						BEGIN
							UPDATE [ptl].[Category]
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