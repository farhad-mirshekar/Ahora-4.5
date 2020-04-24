USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteCategoryMapDiscountByCategoryID'))
	DROP PROCEDURE app.spDeleteCategoryMapDiscountByCategoryID
GO

CREATE PROCEDURE app.spDeleteCategoryMapDiscountByCategoryID
@CategoryID uniqueidentifier
--WITH ENCRYPTION
AS
BEGIN
	DECLARE @ID UNIQUEIDENTIFIER;
	SET @ID = (SELECT ID FROM app.Category_Discount_Mapping WHERE CategoryID = @CategoryID AND [Active] = 1)
	IF @ID IS NOT NULL
		BEGIN
			DELETE FROM app.Category_Discount_Mapping WHERE ID = @ID AND CategoryID = @CategoryID
		END
	RETURN @@ROWCOUNT
END