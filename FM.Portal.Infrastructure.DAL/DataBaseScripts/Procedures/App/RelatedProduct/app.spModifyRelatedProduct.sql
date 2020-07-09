USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spModifyRelatedProduct'))
	DROP PROCEDURE app.spModifyRelatedProduct
GO

CREATE PROCEDURE app.spModifyRelatedProduct
@ID UNIQUEIDENTIFIER,
@ProductID1 UNIQUEIDENTIFIER,
@ProductID2 UNIQUEIDENTIFIER,
@IsNewRecord BIT,
@Priority INT
--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [app].[RelatedProduct]
				(ID,[ProductID1], [ProductID2],CreationDate,[Priority])
			VALUES
				(@ID, @ProductID1 , @ProductID2 , GETDATE() , @Priority)
		END
	ELSE -- update
		BEGIN
			UPDATE [app].[RelatedProduct]
			SET
				[Priority] = @Priority
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END