USE [Ahora]
GO
IF EXISTS(SELECT 1 FROM SYS.PROCEDURES WHERE [object_id] = OBJECT_ID('app.spGetProductAttribute'))
	DROP PROCEDURE app.spGetProductAttribute
GO

CREATE PROCEDURE app.spGetProductAttribute
@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	SELECT 
		attr.*
	FROM	
		[app].[ProductAttribute] attr
	WHERE 
		attr.ID = @ID
END