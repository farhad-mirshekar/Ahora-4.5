USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyDiscount'))
	DROP PROCEDURE app.spModifyDiscount
GO

CREATE PROCEDURE app.spModifyDiscount
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(200),
@DiscountType TINYINT,
@DiscountAmount Decimal(18,3),
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Discount]
				(ID,[Name],[DiscountType]  , [DiscountAmount], CreationDate)
			VALUES
				(@ID, @Name , @DiscountType , @DiscountAmount , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Discount]
			SET
				[Name] = @Name,
				[DiscountType] = @DiscountType,
				[DiscountAmount] = @DiscountAmount
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END