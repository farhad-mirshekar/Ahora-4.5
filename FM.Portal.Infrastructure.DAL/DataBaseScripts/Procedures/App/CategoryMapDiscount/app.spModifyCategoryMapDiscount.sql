USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyCategoryMapDiscount'))
	DROP PROCEDURE app.spModifyCategoryMapDiscount
GO

CREATE PROCEDURE app.spModifyCategoryMapDiscount
@ID UNIQUEIDENTIFIER,
@CategoryID uniqueidentifier,
@DiscountID uniqueidentifier,
@IsNewRecord bit
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			BEGIN TRAN
				UPDATE [app].[Category_Discount_Mapping]
				SET Active = 0
				WHERE CategoryID =@CategoryID
			 
				INSERT INTO [app].[Category_Discount_Mapping]
					([ID],CategoryID,DiscountID,Active,CreationDate)
				VALUES
					(@ID, @CategoryID,@DiscountID,1,GETDATE())
			COMMIT
		END
	RETURN @@ROWCOUNT
END