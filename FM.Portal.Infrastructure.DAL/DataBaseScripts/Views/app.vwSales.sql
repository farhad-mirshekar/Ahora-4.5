IF EXISTS(SELECT 1 FROM sys.views WHERE [object_id] = OBJECT_ID('app.vwSales'))
	DROP VIEW app.vwSales
GO
CREATE VIEW app.vwSales

AS
	SELECT TOP 100 PERCENT
		-----------------BaseDocument
		doc.ID,
		doc.RemoveDate,
		doc.RemoverID,
		Sales.[Type],
		lastFlow.ID LastFlowID,
		lastFlow.SendType LastSendType,
		CAST(COALESCE(lastFlow.FromDocState, 1) AS TINYINT) LastFromDocState,
		CAST(COALESCE(lastFlow.ToDocState, 1) AS TINYINT) LastDocState,
		lastFlow.[Date] LastFlowDate,
		lastFlow.ReadDate LastReadDate,
		lastFlow.FromUserID LastFromUserID,
		lastFlow.ToPositionID LastToPositionID,
		lastFlow.FromPositionID LastFromPositionID,
		---------------------------Payment
		Payment.ID PaymentID,
		Payment.OrderID,
		Payment.Price,
		Payment.RetrivalRefNo,
		Payment.TransactionStatusMessage,
		Payment.CreationDate,

		---------------------------Buyer
		CONCAT(Buyer.FirstName , ' ' , Buyer.LastName) BuyerInfo,
		COALESCE(Buyer.CellPhone , N'شماره همراه وارد نشده است') BuyerPhone
	FROM  app.Sales Sales
	INNER JOIN app.Payment Payment ON Sales.PaymentID = Payment.ID
	INNER JOIN pbl.BaseDocument doc ON doc.ID = Sales.ID
	INNER JOIN pbl.DocumentFlow lastFlow ON lastFlow.DocumentID = doc.ID AND lastFlow.ActionDate IS NULL
	LEFT JOIN org.[User] Buyer ON Payment.UserID = Buyer.ID
	WHERE doc.RemoverID IS NULL
	ORDER BY lastFlow.[Date] DESC