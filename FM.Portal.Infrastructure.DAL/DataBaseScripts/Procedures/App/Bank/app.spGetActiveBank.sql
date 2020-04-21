USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetActiveBank'))
	DROP PROCEDURE app.spGetActiveBank
GO

CREATE PROCEDURE app.spGetActiveBank
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		TOP 1
		bank.*
	FROM	
		[app].[Bank] bank
	WHERE 
		bank.[Default] = 1
END