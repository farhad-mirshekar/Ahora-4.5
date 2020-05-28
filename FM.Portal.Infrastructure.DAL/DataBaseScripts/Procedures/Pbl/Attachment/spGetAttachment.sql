USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetAttachment'))
	DROP PROCEDURE pbl.spGetAttachment
GO

CREATE PROCEDURE pbl.spGetAttachment
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	;WITH Extra AS
	(
		SELECT ID , [Data] FROM pbl.AttachmentExtra WHERE ID = @ID
	)
	SELECT 
		attachment.*,
		extra.Data
	FROM pbl.Attachment	attachment
	INNER JOIN
		Extra extra ON attachment.ID = extra.ID
	WHERE attachment.ID = @ID

	RETURN @@ROWCOUNT
END