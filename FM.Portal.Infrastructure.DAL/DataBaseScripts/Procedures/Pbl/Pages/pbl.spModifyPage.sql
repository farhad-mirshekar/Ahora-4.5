USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyPages'))
	DROP PROCEDURE pbl.spModifyPages
GO

CREATE PROCEDURE pbl.spModifyPages
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@Node HIERARCHYID,
	@ControllerName Nvarchar(200),
	@ActionName Nvarchar(200),
	@Title Nvarchar(200),
	@Description Nvarchar(max),
	@Enabled bit,
	@RouteUrl Nvarchar(max)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRAN
		DECLARE 
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@NewNode HIERARCHYID

	IF @Node IS NULL 
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM pbl.Pages WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM pbl.Pages WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM pbl.Pages WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO pbl.Pages
				(ID, [Node],[ControllerName],[ActionName],[Title],[Description],[Enabled],[CreationDate],[RouteUrl])
				VALUES
				(@ID, @NewNode , @ControllerName , @ActionName , @Title , @Description , @Enabled, GETDATE(),@RouteUrl)
			END
			ELSE
			BEGIN
				UPDATE pbl.Pages
				SET 
					[ControllerName] = @ControllerName ,
					[ActionName] = @ActionName,
					[Title] = @Title,
					[RouteUrl] = @RouteUrl,
					[Description] = @Description
				WHERE ID = @ID

				IF(@Node <> @NewNode)
				BEGIN
					UPDATE pbl.Pages
					SET [Node] = [Node].GetReparentedValue(@Node, @NewNode)
					WHERE [Node].IsDescendantOf(@Node) = 1
				END
			END
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END