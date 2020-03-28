USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductVariantAttribute'))
	DROP PROCEDURE app.spModifyProductVariantAttribute
GO

CREATE PROCEDURE app.spModifyProductVariantAttribute
@ID UNIQUEIDENTIFIER,
@ProductVariantAttributeID UNIQUEIDENTIFIER,
@Name NVARCHAR(400),
@IsPreSelected bit,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ProductVariantAttributeValue]
				(ID,ProductVariantAttributeId,Name,IsPreSelected, CreationDate)
			VALUES
				(@ID, @ProductVariantAttributeID,@Name,@IsPreSelected , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ProductVariantAttributeValue]
			SET
				[Name]=@Name,
				[IsPreSelected] = @IsPreSelected
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END