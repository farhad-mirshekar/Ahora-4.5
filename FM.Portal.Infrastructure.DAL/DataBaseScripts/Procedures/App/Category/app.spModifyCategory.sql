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
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Category]
				(ID,[Title],[ParentID], [IncludeInTopMenu],[IncludeInLeftMenu],[HasDiscountsApplied], CreationDate)
			VALUES
				(@ID, @Title , @ParentID , @IncludeInTopMenu,@IncludeInLeftMenu,@HasDiscountsApplied , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Category]
			SET
				[ParentID]=@ParentID ,
				[IncludeInTopMenu] = @IncludeInTopMenu ,
				[IncludeInLeftMenu] = @IncludeInLeftMenu ,
				[HasDiscountsApplied] = @HasDiscountsApplied,
				[Title] = @Title
			WHERE
				[ID] = @ID
			IF @HasDiscountsApplied = 0
				BEGIN
					EXEC app.spDeleteCategoryMapDiscountByCategoryID @ID
				END
		END

	RETURN @@ROWCOUNT
END