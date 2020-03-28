USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('org.spModifyUser'))
	DROP PROCEDURE org.spModifyUser
GO

CREATE PROCEDURE org.spModifyUser
@ID uniqueidentifier,
@isNewRecord bit,
@Enabled bit,
@Username varchar(50),
@Password varchar(100),
@FirstName nvarchar(50),
@LastName nvarchar(50),
@NationalCode varchar(18),
@Email varchar(256),
@EmailVerified bit,
@CellPhone varchar(20),
@CellPhoneVerified bit,
@PasswordExpireDate smalldatetime

--WITH ENCRYPTION
AS
BEGIN
	IF @IsNewRecord = 1 --insert
		BEGIN
			INSERT INTO [org].[user]
				(
					[ID] ,
					[Enabled] ,
					[Username] ,
					[Password] ,
					[FirstName] ,
					[LastName] ,
					[NationalCode] ,
					[Email] ,
					[EmailVerified] ,
					[CellPhone] ,
					[CellPhoneVerified] ,
					[PasswordExpireDate]
				)
			VALUES
				(
				@ID ,
				@Enabled ,
				@Username ,
				@Password ,
				@FirstName ,
				@LastName ,
				@NationalCode ,
				@Email ,
				@EmailVerified ,
				@CellPhone,
				@CellPhoneVerified ,
				@PasswordExpireDate 
				)
		END
	ELSE -- update
		BEGIN
			UPDATE [org].[User]
			SET
				[Enabled] = @Enabled,
				FirstName = @FirstName,
				LastName = @LastName,
				NationalCode = @NationalCode,
				Email = @Email,
				CellPhone = @CellPhone,
				EmailVerified = @EmailVerified,
				CellPhoneVerified = @CellPhoneVerified
			WHERE
				[ID] = @ID
		END
END