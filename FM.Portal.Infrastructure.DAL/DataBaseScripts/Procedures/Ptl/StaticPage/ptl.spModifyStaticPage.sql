﻿USE [Ahora]
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
	@AttachmentID UNIQUEIDENTIFIER,
	@Name NVARCHAR(1000),
	@UrlDesc NVARCHAR(1000),
	@Enabled TINYINT,
	@UserID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	UPDATE ptl.StaticPage
		SET 
			[Body] = @Body,
			[Description] = @Description,
			[MetaKeywords] = @MetaKeywords,
			[BannerShow] = @BannerShow,
			[AttachmentID] = @AttachmentID
		WHERE ID = @ID

	UPDATE ptl.[Pages]
		SET 
			[Name] = @Name,
			[UrlDesc] = @UrlDesc,
			[Enabled] = @Enabled,
			[UserID] = @UserID
		WHERE ID = @ID
	RETURN @@ROWCOUNT
END