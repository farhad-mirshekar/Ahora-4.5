USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteShoppingCartItem'))
	DROP PROCEDURE app.spDeleteShoppingCartItem
GO

CREATE PROCEDURE app.spDeleteShoppingCartItem
@ShoppingID UniqueIdentifier,
@ProductID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM app.ShoppingCartItem 
	WHERE
		ShoppingID = @ShoppingID 
		AND ProductID = @ProductID 

	RETURN @@ROWCOUNT
END