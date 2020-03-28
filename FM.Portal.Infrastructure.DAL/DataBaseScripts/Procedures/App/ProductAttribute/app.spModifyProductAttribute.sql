USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductAttribute'))
	DROP PROCEDURE app.spModifyProductAttribute
GO

CREATE PROCEDURE app.spModifyProductAttribute
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(Max),
@Description NVARCHAR(Max),
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ProductAttribute]
				(ID,[Name] , [Description] , CreationDate)
			VALUES
				(@ID, @Name,@Description , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ProductAttribute]
			SET
				Name = @Name,
				[Description] = @Description
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END