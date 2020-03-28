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

		
	SELECT ID
			, ParentID  
			, [Type]
			, [FileName]
			, Comment
			, [Data]
			, [CreationDate]
			,[PathType]
	FROM pbl.Attachment	
	WHERE ID = @ID

	RETURN @@ROWCOUNT
END