USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifyCategory'))
	DROP PROCEDURE ptl.spModifyCategory
GO

CREATE PROCEDURE ptl.spModifyCategory
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(50),
@IncludeInTopMenu bit,
@IncludeInLeftMenu bit,
@ParentID UNIQUEIDENTIFIER,
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[Category]
				(ID,[Title],[ParentID], [IncludeInTopMenu],[IncludeInLeftMenu], CreationDate)
			VALUES
				(@ID, @Title , @ParentID , @IncludeInTopMenu,@IncludeInLeftMenu , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[Category]
			SET
				[ParentID]=@ParentID ,
				[IncludeInTopMenu] = @IncludeInTopMenu ,
				[IncludeInLeftMenu] = @IncludeInLeftMenu ,
				[Title] = @Title
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END