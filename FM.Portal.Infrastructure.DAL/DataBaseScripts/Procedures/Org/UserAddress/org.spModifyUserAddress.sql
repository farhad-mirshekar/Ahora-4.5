USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('org.spModifyUserAddress'))
	DROP PROCEDURE org.spModifyUserAddress
GO

CREATE PROCEDURE org.spModifyUserAddress
@ID uniqueidentifier,
@IsNewRecord bit,
@Address nvarchar(max),
@UserID uniqueidentifier,
@PostalCode nvarchar(20)

--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [org].[UserAddress]
				(
					[ID] ,
					[UserID] ,
					[Address] ,
					[PostalCode],
					[CreationDate]
				)
			VALUES
				(
				@ID ,
				@UserID ,
				@Address ,
				@PostalCode,
				GETDATE()
				)
		END
	ELSE -- update
		BEGIN
			UPDATE [org].[UserAddress]
			SET
				[Address] = @Address,
				[PostalCode] = @PostalCode
			WHERE
				[ID] = @ID
		END

	RETURN @@ROWCOUNT
END