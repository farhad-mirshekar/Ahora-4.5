USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyMenu'))
	DROP PROCEDURE pbl.spModifyMenu
GO

CREATE PROCEDURE pbl.spModifyMenu
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@Node HIERARCHYID,
	@Name Nvarchar(256),
	@Enabled bit,
	@Url Nvarchar(max),
	@IconText Nvarchar(256),
	@Priority Int,
	@Parameters Nvarchar(Max)
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRAN
		DECLARE 
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@NewNode HIERARCHYID

	IF @Node IS NULL 
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM pbl.Menu WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM pbl.Menu WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM pbl.Menu WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO pbl.Menu
				(ID, [Node],[Name],[Enabled],[CreationDate],[Url],[IconText],[Priority],[Parameters])
				VALUES
				(@ID, @NewNode , @Name ,  @Enabled, GETDATE(),@Url , @IconText , @Priority , @Parameters)
			END
			ELSE
			BEGIN
				UPDATE pbl.Menu
				SET 
					[Name] = @Name,
					[Url] = @Url,
					[IconText] = @IconText,
					[Priority] = @Priority,
					[Enabled] = @Enabled,
					[Parameters] = @Parameters
				WHERE ID = @ID

				IF(@Node <> @NewNode)
				BEGIN
					UPDATE pbl.Menu
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