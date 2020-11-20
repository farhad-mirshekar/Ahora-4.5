USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductVariantAttributeValue'))
	DROP PROCEDURE app.spModifyProductVariantAttributeValue
GO

CREATE PROCEDURE app.spModifyProductVariantAttributeValue
@ID UNIQUEIDENTIFIER,
@ProductVariantAttributeID UNIQUEIDENTIFIER,
@Name NVARCHAR(400),
@IsPreSelected bit,
@IsNewRecord bit,
@Price Money
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[ProductVariantAttributeValue]
				(ID,ProductVariantAttributeId,Name,IsPreSelected, CreationDate , Price)
			VALUES
				(@ID, @ProductVariantAttributeID,@Name,@IsPreSelected , GETDATE() , @Price)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[ProductVariantAttributeValue]
			SET
				[Name]=@Name,
				[IsPreSelected] = @IsPreSelected,
				[Price] = @Price
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END