IF EXISTS(SELECT 1 FROM sys.views WHERE [object_id] = OBJECT_ID('app.vwPayment'))
	DROP VIEW app.vwPayment
GO

CREATE VIEW app.vwPayment

AS
	SELECT TOP 100 PERCENT
		-----------------BaseDocument
		doc.ID,
		--doc.CreationDate,
		doc.[Type] DocumentType,
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
		Payment.OrderID,
		Payment.Price,
		Payment.RetrivalRefNo,
		Payment.TransactionStatus,
		Payment.CreationDate
	FROM  app.Payment Payment
	INNER JOIN pbl.BaseDocument doc ON doc.PaymentID = Payment.ID
	INNER JOIN pbl.DocumentFlow lastFlow ON lastFlow.DocumentID = doc.ID AND lastFlow.ActionDate IS NULL
	WHERE doc.RemoverID IS NULL
	ORDER BY lastFlow.[Date] DESC