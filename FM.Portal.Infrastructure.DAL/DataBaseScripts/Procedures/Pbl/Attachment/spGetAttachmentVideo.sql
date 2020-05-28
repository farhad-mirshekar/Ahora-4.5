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

	;WITH Extra AS
	(
		SELECT ID,[Data] FROM pbl.AttachmentExtra WHERE ParentID = @ParentID
	)
	SELECT 
		attachment.ID
		, attachment.ParentID  
		, attachment.[Type]
		, attachment.[FileName]
		, attachment.Comment
		, extra.[Data]
		, attachment.[CreationDate]
		,attachment.[PathType]
	FROM 
		pbl.Attachment attachment
	INNER JOIN
		Extra extra ON attachment.ID = extra.ID	
	WHERE 
		ParentID = @ParentID AND 
		PathType = 7

	RETURN @@ROWCOUNT
END