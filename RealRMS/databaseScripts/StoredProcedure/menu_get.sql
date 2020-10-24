IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'menu_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.menu_get AS RAISERROR(''Procedure menu_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE menu_get
@id		INT

AS

SELECT menu.id, Menu.Description, Menu.Price, Menu.name, MenuCategory.Name as categoryName,
MenuCategory.Id as CategoryId
FROM Menu
INNER JOIN MenuCategory
ON Menu.MenuCategoryId = MenuCategory.Id
WHERE menu.id = @id;

GO

GRANT EXECUTE ON menu_get TO sqlUser