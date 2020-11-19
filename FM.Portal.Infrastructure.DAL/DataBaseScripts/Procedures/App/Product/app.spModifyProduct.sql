USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyProduct'))
	DROP PROCEDURE app.spModifyProduct
GO

CREATE PROCEDURE app.spModifyProduct
@ID UNIQUEIDENTIFIER,
@Name NVARCHAR(200),
@ShortDescription NVARCHAR(max),
@FullDescription NVARCHAR(max),
@ShowOnHomePage bit,
@MetaKeywords NVARCHAR(400),
@MetaDescription NVARCHAR(max),
@MetaTitle NVARCHAR(400),
@AllowCustomerReviews bit,
@CallForPrice bit,
@Price decimal(18,3),
@Discount decimal(18,3),
@SpecialOffer bit,
@Weight decimal(18,3),
@Length decimal(18,3),
@Width decimal(18,3),
@Height decimal(18,3),
@Published decimal(18,3),
@UserID uniqueidentifier,
@CategoryID uniqueidentifier,
@isNewRecord bit,
@StockQuantity int,
@DiscountType tinyint,
@HasDiscount bit,
@IsDownload bit,
@ShippingCostID UNIQUEIDENTIFIER,
@DeliveryDateID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	IF @isNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[Product]
				(ID,
				[Name],
				[ShortDescription],
				[FullDescription],
				[ShowOnHomePage],
				[MetaKeywords],
				[MetaDescription],
				[MetaTitle],
				[ApprovedRatingSum],
				[AllowCustomerReviews],
				[NotApprovedRatingSum],
				[ApprovedTotalReviews],
				[NotApprovedTotalReviews],
				[CallForPrice],
				[Price],
				[Discount],
				[SpecialOffer],
				[Weight],
				[Length],
				[Width],
				[Height],
				[Published],
				[UserID],
				[CategoryID],
				[CreationDate],
				[UpdatedDate],
				[StockQuantity],
				[DiscountType],
				[HasDiscount],
				[IsDownload],
				[ShippingCostID],
				[DeliveryDateID])
			VALUES
				(@ID,
				@Name,
				@ShortDescription,
				@FullDescription,
				@ShowOnHomePage,
				@MetaKeywords,
				@MetaDescription,
				@MetaTitle,
				@AllowCustomerReviews,
				0,
				0,
				0,
				0,
				@CallForPrice,
				@Price,
				@Discount,
				@SpecialOffer,
				@Weight,
				@Length,
				@Width,
				@Height,
				@Published,
				@UserID,
				@CategoryID,
				GETDATE(),
				GETDATE(),
				@StockQuantity,
				@DiscountType,
				@HasDiscount,
				@IsDownload,
				@ShippingCostID,
				@DeliveryDateID)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[Product]
			SET
				[Name] = @Name,
				[ShortDescription] = @ShortDescription,
				[FullDescription] = @FullDescription,
				[ShowOnHomePage] = @ShowOnHomePage,
				[MetaKeywords] = @MetaKeywords,
				[MetaDescription] = @MetaDescription,
				[MetaTitle] = @MetaTitle,
				[AllowCustomerReviews] = @AllowCustomerReviews,
				[CallForPrice] = @CallForPrice,
				[Price] = @Price,
				[Discount] = @Discount,
				[SpecialOffer] = @SpecialOffer,
				[Weight] = @Weight,
				[Length] = @Length,
				[Width] = @Width,
				[Height] = @Height,
				[Published] = @Published,
				[UserID] = @UserID,
				[CategoryID] = @CategoryID,
				[UpdatedDate] = GETDATE(),
				[StockQuantity] = @StockQuantity,
				[DiscountType] = @DiscountType,
				[HasDiscount] = @HasDiscount,
				[IsDownload] = @IsDownload,
				[ShippingCostID] = @ShippingCostID,
				[DeliveryDateID] = @DeliveryDateID
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END