USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spDeleteEvents'))
	DROP PROCEDURE ptl.spDeleteEvents
GO

CREATE PROCEDURE ptl.spDeleteEvents
@ID UNIQUEIDENTIFIER,
@RemoverID UNIQUEIDENTIFIER
AS
BEGIN
	--DELETE FROM [ptl].[Events] WHERE ID = @ID
	UPDATE ptl.[Events]
	SET
		RemoverID = @RemoverID,
		RemoverDate = GETDATE()
	WHERe
		ID = @ID
	RETURN @@ROWCOUNT
END