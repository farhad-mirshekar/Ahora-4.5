USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetDiscount'))
	DROP PROCEDURE app.spGetDiscount
GO

CREATE PROCEDURE app.spGetDiscount
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		disc.*
	FROM	
		[app].[Discount] disc
	WHERE 
		disc.ID = @ID
END