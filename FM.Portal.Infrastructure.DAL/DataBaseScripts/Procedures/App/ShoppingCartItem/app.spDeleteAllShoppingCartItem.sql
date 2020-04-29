USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spDeleteAllShoppingCartItem'))
	DROP PROCEDURE app.spDeleteAllShoppingCartItem
GO

CREATE PROCEDURE app.spDeleteAllShoppingCartItem
@ShoppingID UniqueIdentifier
--WITH ENCRYPTION
AS
BEGIN
		DELETE FROM app.ShoppingCartItem 
		WHERE
			ShoppingID = @ShoppingID
END