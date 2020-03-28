USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetShoppingCartItemByProductID'))
	DROP PROCEDURE app.spGetShoppingCartItemByProductID
GO

CREATE PROCEDURE app.spGetShoppingCartItemByProductID
@ShoppingID UniqueIdentifier,
@ProductID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN

	SELECT 
		*
	FROM 
		[app].[ShoppingCartItem]
	WHERE 
		[ShoppingID] = @ShoppingID AND
		[ProductID] = @ProductID

END