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
		DELETE FROM app.ShoppingCartItem 
		WHERE
			ShoppingID = @ShoppingID AND
			ProductID = @ProductID 

	;WITH main AS(
		SELECT * FROM app.ShoppingCartItem WHERE ShoppingID = @ShoppingID
	)

	SELECT * 
	FROM main
	ORDER BY [CreationDate]
END