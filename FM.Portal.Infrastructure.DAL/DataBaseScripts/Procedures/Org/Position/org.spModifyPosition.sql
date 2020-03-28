USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('org.spModifyPosition'))
	DROP PROCEDURE org.spModifyPosition
GO

CREATE PROCEDURE org.spModifyPosition
	@IsNewRecord BIT,
	@ID UNIQUEIDENTIFIER,
	@ParentID UNIQUEIDENTIFIER,
	@ApplicationID UNIQUEIDENTIFIER,
	@DepartmentID UNIQUEIDENTIFIER,
	@UserID UNIQUEIDENTIFIER,
	@Type TINYINT,
	@RoleIDs NVARCHAR(MAX)
--WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE   
		@ParentNode HIERARCHYID,
		@LastChildNode HIERARCHYID,
		@Node HIERARCHYID

	BEGIN TRY
		BEGIN TRAN

			SET @Node = (SELECT [Node] FROM org.Position WHERE ID = @ID)
				
			IF @Node IS NULL OR @ParentID <> (SELECT TOP 1 ID FROM org.Position where @Node.GetAncestor(1) = [Node])
			BEGIN
				IF @ParentID = 0x
					SET @ParentNode = HIERARCHYID::GetRoot()  
				ELSE
					SET @ParentNode = (SELECT [Node] FROM org.Position WHERE ID = @ParentID)
				SET @LastChildNode = (SELECT MAX([Node]) FROM org.Position WHERE [Node].GetAncestor(1) = @ParentNode AND RemoverID IS NULL)
				SET @Node = @ParentNode.GetDescendant(@LastChildNode, NULL)
			END 

			IF @IsNewRecord = 1 -- insert
			BEGIN
				INSERT INTO org.Position
				(ID, [Node], ApplicationID, DepartmentID, UserID, [Type], [Default], [Enabled])
				VALUES
				(@ID, @Node, @ApplicationID, @DepartmentID, @UserID, @Type, 0, 1)
			END
			ELSE
			BEGIN -- update
				UPDATE org.Position
				SET [Node] = @Node, UserID = @UserID , [Type] = @Type, [Enabled] = 1
				WHERE ID = @ID
			END

			DELETE FROM org.PositionRole where PositionID = @ID

			IF @RoleIDs IS NOT NULL
			BEGIN

				INSERT INTO org.PositionRole(ID, PositionID, RoleID)
				SELECT 
					NEWID() ID,
					@ID PositionID,
					splitdata AS RoleID
				FROM dbo.fnSplitString(@RoleIDs,',')
				
			END
		COMMIT
	END TRY
	BEGIN CATCH
		;THROW
	END CATCH
	
	RETURN @@ROWCOUNT
END