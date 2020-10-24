IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'menu_update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.menu_update AS RAISERROR(''Procedure menu_update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE menu_update
		@Name	                        VARCHAR(1000),
		@MenuCategoryId					INT,
		@Description					VARCHAR(1000),
		@Price						    FLOAT,
		@id								INT
AS

UPDATE Menu
SET name = @name, menuCategoryId = @menuCategoryId, price = @price, description = @description
WHERE id = @id

GO

GRANT EXECUTE ON menu_update TO sqlUser