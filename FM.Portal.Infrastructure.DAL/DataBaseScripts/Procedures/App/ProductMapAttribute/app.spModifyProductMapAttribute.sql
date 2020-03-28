USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProductMapAttribute'))
	DROP PROCEDURE app.spModifyProductMapAttribute
GO

CREATE PROCEDURE app.spModifyProductMapAttribute
@ID UNIQUEIDENTIFIER,
@ProductID UNIQUEIDENTIFIER,
@ProductAttributeID UNIQUEIDENTIFIER,
@TextPrompt NVARCHAR(Max),
@IsRequired bit,
@AttributeControlType tinyint,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Product_ProductAttribute_Mapping]
				(ID,ProductID,ProductAttributeID,TextPrompt,IsRequired,AttributeControlType, CreationDate)
			VALUES
				(@ID, @ProductID,@ProductAttributeID,@TextPrompt,@IsRequired,@AttributeControlType , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Product_ProductAttribute_Mapping]
			SET
				[TextPrompt]=@TextPrompt,
				[IsRequired] = @IsRequired
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END