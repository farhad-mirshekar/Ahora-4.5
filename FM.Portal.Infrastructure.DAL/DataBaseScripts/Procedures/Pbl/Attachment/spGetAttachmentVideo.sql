USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spGetAttachmentVideo'))
	DROP PROCEDURE pbl.spGetAttachmentVideo
GO

CREATE PROCEDURE pbl.spGetAttachmentVideo
	@ParentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

		
	SELECT 
		ID
		, ParentID  
		, [Type]
		, [FileName]
		, Comment
		, [Data]
		, [CreationDate]
		,[PathType]
	FROM 
		pbl.Attachment	
	WHERE 
		ParentID = @ParentID AND 
		PathType = 7

	RETURN @@ROWCOUNT
END