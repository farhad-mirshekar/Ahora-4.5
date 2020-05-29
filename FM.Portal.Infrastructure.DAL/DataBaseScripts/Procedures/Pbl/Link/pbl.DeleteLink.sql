﻿USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('pbl.spDeleteLink'))
	DROP PROCEDURE pbl.spDeleteLink
GO

CREATE PROCEDURE pbl.spDeleteLink
	@ID UNIQUEIDENTIFIER
--WITH ENCRYPTION
AS
BEGIN
	--SET NOCOUNT ON;
	DELETE FROM pbl.Link WHERE ID = @ID
	RETURN @@ROWCOUNT

END