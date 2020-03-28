USE [Ahora]

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetAttachmentsByParentIDs'))
	DROP PROCEDURE pbl.spGetAttachmentsByParentIDs
GO

CREATE PROCEDURE pbl.spGetAttachmentsByParentIDs
	@ParentID Uniqueidentifier
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	SELECT
		attachment.ID,
		attachment.[FileName],
		attachment.ParentID,
		attachment.[Type],
		attachment.Comment,
		attachment.PathType
		--CASE WHEN @ReturnData = 1 THEN attachment.[Data] ELSE NULL END [Data]
	FROM pbl.Attachment attachment
	WHERE ParentID =@ParentID

	RETURN @@ROWCOUNT
END