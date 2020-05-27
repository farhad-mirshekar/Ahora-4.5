USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spModifyDynamicPage'))
	DROP PROCEDURE ptl.spModifyDynamicPage
GO

CREATE PROCEDURE ptl.spModifyDynamicPage
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@Body NVARCHAR(MAX),
	@Description NVARCHAR(1000),
	@MetaKeywords NVARCHAR(1000),
	@UrlDesc NVARCHAR(1000),
	@PageID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@IsShow TINYINT,
	@Name NVARCHAR(2000)
--WITH ENCRYPTION
AS
BEGIN

	IF @IsNewRecord = 1 -- insert
			BEGIN
				DECLARE @TrackingCode Nvarchar(20)
				SET @TrackingCode = (SELECT STR(FLOOR(RAND(CHECKSUM(NEWID()))*(9999999999-1000000000+1)+1000000000)))

				INSERT INTO ptl.DynamicPage
				(ID,[TrackingCode],[Body],[Description],[PageID],[MetaKeywords],[IsShow],[UserID],[UrlDesc],[CreationDate],[Name])
				VALUES
				(@ID, @TrackingCode,@Body,@Description,@PageID,@MetaKeywords,@IsShow,@UserID,@UrlDesc,GETDATE(),@Name)
			END
			ELSE
			BEGIN
				UPDATE ptl.DynamicPage
				SET 
					[Body] = @Body,
					[Description] = @Description,
					[MetaKeywords] = @MetaKeywords,
					[IsShow] = @IsShow,
					[UrlDesc] = @UrlDesc,
					[Name] = @Name
				WHERE ID = @ID

			END
	RETURN @@ROWCOUNT
END