IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'menu_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.menu_insert AS RAISERROR(''Procedure menu_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE menu_insert
		@Name	                        VARCHAR(1000),
		@MenuCategoryId					INT,
		@Description					VARCHAR(1000) = NULL,
		@Price						    FLOAT
AS

INSERT INTO Menu(Name, MenuCategoryId, Description, Price)
VALUES (@Name, @MenuCategoryId,@Description,@Price);

SELECT SCOPE_IDENTITY();

GO

GRANT EXECUTE ON menu_insert TO sqlUser