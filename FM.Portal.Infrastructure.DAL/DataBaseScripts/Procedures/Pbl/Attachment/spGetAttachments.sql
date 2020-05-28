USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetAttachments'))
	DROP PROCEDURE pbl.spGetAttachments
GO

CREATE PROCEDURE pbl.spGetAttachments
	@ParentID UNIQUEIDENTIFIER,
	@Type TINYINT,
	@ReturnData BIT
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	
	SELECT ID,
		ParentID,
		[Type],
		[FileName],
		Comment,
		[PathType]
		--CASE WHEN @ReturnData = 1 THEN attachment.[Data] ELSE NULL END [Data]
	FROM pbl.Attachment
	WHERE ParentID = @ParentID
		AND (@Type < 1 OR [Type] = @Type)

	RETURN @@ROWCOUNT
END