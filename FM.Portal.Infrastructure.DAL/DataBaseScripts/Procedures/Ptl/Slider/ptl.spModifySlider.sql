USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('ptl.spModifySlider'))
	DROP PROCEDURE ptl.spModifySlider
GO

CREATE PROCEDURE ptl.spModifySlider
@ID UNIQUEIDENTIFIER,
@Title NVARCHAR(max),
@Priority int,
@Url NVARCHAR(max),
@Enabled TINYINT,
@IsNewRecord BIT
WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [ptl].[Slider]
				([ID],[Title],[Priority],[Url],[Enabled],[CreationDate])
			VALUES
				(@ID , @Title , @Priority , @Url , @Enabled , GETDATE())
		END
	ELSE -- update
		BEGIN
			UPDATE [ptl].[Slider]
			SET
				[Title] = @Title ,
				[Priority] = @Priority,
				[Url] = @Url,
				[Enabled] = @Enabled
			WHERE
				[ID] = @ID
		END
			RETURN @@ROWCOUNT
END
