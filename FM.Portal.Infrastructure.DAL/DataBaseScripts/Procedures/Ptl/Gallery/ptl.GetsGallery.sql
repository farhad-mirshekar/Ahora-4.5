USE [Ahora]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE [object_id] = OBJECT_ID('ptl.spGetsGallery'))
	DROP PROCEDURE ptl.spGetsGallery
GO

CREATE PROCEDURE [ptl].spGetsGallery
@Name NVARCHAR(100),
@PageSize INT,
@PageIndex INT
--WITH ENCRYPTION
AS
BEGIN
	SET @PageSize = COALESCE(@PageSize , 5)
	SET @PageIndex = COALESCE(@PageIndex,1)

	IF @PageIndex = 0 
	BEGIN
		SET @PageSize = 10000000
		SET @PageIndex = 1
	END
	;WITH Attachment AS
	(
		SELECT 
			Attachment.[FileName],
			Attachment.PathType,
			Attachment.ParentID
		FROM pbl.Attachment Attachment
		INNER JOIN ptl.Gallery Gallery ON Attachment.ParentID = Gallery.ID
		WHERE Attachment.[Type] = 1 -- original pic
	), MainSelect AS
	(
		SELECT
			gallery.*,
			CONCAT(CreatorUser.FirstName , ' ' , CreatorUser.LastName) AS CreatorName,
			Attachment.PathType,
			Attachment.[FileName]
		FROM ptl.Gallery gallery
		INNER JOIN 
			org.[User] CreatorUser ON gallery.UserID = CreatorUser.ID
		LEFT JOIN	
			 Attachment Attachment ON Attachment.ParentID = gallery.ID
		WHERE
			(@Name IS NULL OR gallery.[Name] LIKE CONCAT('%' , @Name , '%'))
	),TempCount AS
	(
		SELECT
			COUNT(*) AS Total
		FROM MainSelect
	)
	SELECT *
	FROM MainSelect , TempCount
	ORDER BY 
		CreationDate Desc
	OFFSET((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY
	OPTION(RECOMPILE) 
END