USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyCategory'))
	DROP PROCEDURE app.spModifyCategory
GO

CREATE PROCEDURE app.spModifyCategory
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(50),
@IncludeInTopMenu bit,
@IncludeInLeftMenu bit,
@ParentID UNIQUEIDENTIFIER,
@HasDiscountsApplied bit,
@isNewRecord bit,
@Node HIERARCHYID
--WITH ENCRYPTION
AS
BEGIN
	SET XACT_ABORT ON;

	DECLARE 
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@NewNode HIERARCHYID

	IF EXISTS(SELECT 1 FROM app.Category WHERE ID <> @ID AND REPLACE([Title], ' ', '') = REPLACE(@Title, ' ', ''))
		THROW 50000, N'نام تکراری است', 1

	IF @Node IS NULL 
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM app.Category WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM app.Category WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM app.Category WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END
	BEGIN TRY
		BEGIN TRAN
	      IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Category]
				(ID,[Title], [IncludeInTopMenu],[IncludeInLeftMenu],[HasDiscountsApplied], CreationDate,[Node])
			VALUES
				(@ID, @Title , @IncludeInTopMenu,@IncludeInLeftMenu,@HasDiscountsApplied , GETDATE(),@NewNode)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Category]
			SET
				[IncludeInTopMenu] = @IncludeInTopMenu ,
				[IncludeInLeftMenu] = @IncludeInLeftMenu ,
				[HasDiscountsApplied] = @HasDiscountsApplied,
				[Title] = @Title
			WHERE
				[ID] = @ID
		    IF(@Node <> @NewNode)
				BEGIN
					UPDATE [app].[Category]
					SET [Node] = [Node].GetReparentedValue(@Node, @NewNode)
					WHERE [Node].IsDescendantOf(@Node) = 1
			    END

			IF @HasDiscountsApplied = 0
				BEGIN
					EXEC app.spDeleteCategoryMapDiscountByCategoryID @ID
				END
		END
	COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	RETURN @@ROWCOUNT
END