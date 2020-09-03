USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetsDocumentFlow'))
	DROP PROCEDURE pbl.spGetsDocumentFlow
GO

CREATE PROCEDURE pbl.spGetsDocumentFlow
	@DocumentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CONCAT(fromUser.FirstName , ' ' , fromUser.LastName) AS FromUserFullName,
		CONCAT(ToUser.FirstName , ' ' , ToUser.LastName) AS ToUserFullName,
		fromPosition.[Type] FromUserPositionType,
		FromDepartment.[Name] FromDepartmentName,
		toPosition.[Type] ToUserPositionType,
		ToDepartment.ID ToDepartmentID,
		ToDepartment.[Name] ToDepartmentName,
		flow.ReadDate,
		flow.ToPositionID,
		flow.ToDocState,
		flow.FromPositionID,
		flow.SendType,
		flow.[Date],
		flow.ActionDate,
		flow.Comment
	FROM pbl.DocumentFlow flow
	INNER JOIN pbl.BaseDocument document on document.ID = flow.DocumentID
	INNER JOIN org.[User] fromUser ON fromUser.ID = flow.FromUserID
	LEFT JOIN [org].[Position] fromPosition ON fromPosition.ID = flow.FromPositionID
	LEFT JOIN [org].Department FromDepartment ON FromDepartment.ID = fromPosition.DepartmentID
	LEFT JOIN [org].[Position] toPosition ON toPosition.ID = flow.ToPositionID
	LEFT JOIN [org].Department ToDepartment ON ToDepartment.ID = ToPosition.DepartmentID
	LEFT JOIN [org].[User] ToUser ON toPosition.UserID = ToUser.ID
	WHERE flow.DocumentID = @DocumentID
	ORDER BY [Date] , [ToDocState] ASC
	RETURN @@ROWCOUNT
END