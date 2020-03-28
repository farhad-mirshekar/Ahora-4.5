USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetCategory'))
	DROP PROCEDURE app.spGetCategory
GO

CREATE PROCEDURE app.spGetCategory
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		cat.*
	FROM	
		[app].[Category] cat
	WHERE 
		cat.ID = @ID
END