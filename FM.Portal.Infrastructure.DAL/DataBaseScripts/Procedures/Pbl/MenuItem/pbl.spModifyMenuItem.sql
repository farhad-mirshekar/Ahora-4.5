USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spModifyMenuItem'))
	DROP PROCEDURE pbl.spModifyMenuItem
GO

CREATE PROCEDURE pbl.spModifyMenuItem
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@MenuID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@Node HIERARCHYID,
	@Name Nvarchar(256),
	@Enabled bit,
	@Url Nvarchar(max),
	@IconText Nvarchar(256),
	@Priority Int,
	@Parameters Nvarchar(Max),
	@ForeignLink TINYINT
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
		OR @ParentID <> COALESCE((SELECT TOP 1 ID FROM pbl.MenuItem WHERE @Node.GetAncestor(1) = [Node]), 0x)
	BEGIN
		SET @ParentNode = COALESCE((SELECT [Node] FROM pbl.MenuItem WHERE ID = @ParentID), HIERARCHYID::GetRoot())
		SET @LastChildNode = (SELECT MAX([Node]) FROM pbl.MenuItem WHERE [Node].GetAncestor(1) = @ParentNode)
		SET @NewNode = @ParentNode.GetDescendant(@LastChildNode, NULL)
	END
			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO pbl.MenuItem
				(ID,[MenuID], [Node],[Name],[Enabled],[CreationDate],[Url],[IconText],[Priority],[Parameters],[ForeignLink])
				VALUES
				(@ID, @MenuID,@NewNode , @Name ,  @Enabled, GETDATE(),@Url , @IconText , @Priority , @Parameters,@ForeignLink)
			END
			ELSE
			BEGIN
				UPDATE pbl.MenuItem
				SET 
					[Name] = @Name,
					[Url] = @Url,
					[IconText] = @IconText,
					[Priority] = @Priority,
					[Enabled] = @Enabled,
					[Parameters] = @Parameters,
					[ForeignLink] = @ForeignLink
				WHERE ID = @ID

				IF(@Node <> @NewNode)
				BEGIN
					UPDATE pbl.MenuItem
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