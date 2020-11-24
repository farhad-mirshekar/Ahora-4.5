USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spModifyStaticPage'))
	DROP PROCEDURE ptl.spModifyStaticPage
GO

CREATE PROCEDURE ptl.spModifyStaticPage
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Body NVARCHAR(MAX),
	@Description NVARCHAR(1000),
	@MetaKeywords NVARCHAR(1000),
	@BannerShow TINYINT,
	@AttachmentID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	BEGIN TRAN
	   UPDATE ptl.StaticPage
		SET 
			[Body] = @Body,
			[Description] = @Description,
			[MetaKeywords] = @MetaKeywords,
			[BannerShow] = @BannerShow,
			[AttachmentID] = @AttachmentID
		WHERE ID = @ID
	COMMIT
	RETURN @@ROWCOUNT
END