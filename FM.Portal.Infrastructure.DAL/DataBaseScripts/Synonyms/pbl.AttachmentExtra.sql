USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.synonyms WHERE [object_id] = OBJECT_ID('pbl.AttachmentExtra'))
	DROP SYNONYM pbl.AttachmentExtra

CREATE SYNONYM pbl.AttachmentExtra
	FOR [Ahora.Extra].[pbl].[Attachment]
GO