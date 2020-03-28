USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyDiscount'))
	DROP PROCEDURE app.spModifyDiscount
GO

CREATE PROCEDURE app.spModifyDiscount
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(200),
@DiscountType TINYINT,
@DiscountPercentage int,
@DiscountAmount Decimal(18,3),
@isNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Discount]
				(ID,[Name],[DiscountType] ,[DiscountPercentage] , [DiscountAmount], CreationDate)
			VALUES
				(@ID, @Name , @DiscountType , @DiscountPercentage , @DiscountAmount , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Discount]
			SET
				[Name] = @Name,
				[DiscountType] = @DiscountType,
				[DiscountPercentage] = @DiscountPercentage,
				[DiscountAmount] = @DiscountAmount
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END