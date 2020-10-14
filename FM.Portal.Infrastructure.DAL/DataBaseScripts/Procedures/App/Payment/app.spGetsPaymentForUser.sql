USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetsPaymentForUser'))
	DROP PROCEDURE app.spGetsPaymentForUser
GO

CREATE PROCEDURE app.spGetsPaymentForUser
@UserID UNIQUEIDENTIFIER,
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN

	;WITH MainSelect AS
	(
		SELECT 
			DISTINCT
			Payment.*,
			orders.TrackingCode
		FROM	
			[app].[Payment] payment
		INNER JOIN
			[app].[Order] orders ON payment.OrderID = orders.ID
		WHERE 
			payment.TransactionStatus = 0 
			AND orders.UserID = @UserID
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM
			MainSelect
	)
	
	SELECT * FROM MainSelect, TempCount						
	ORDER BY CreationDate DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION (RECOMPILE);
END