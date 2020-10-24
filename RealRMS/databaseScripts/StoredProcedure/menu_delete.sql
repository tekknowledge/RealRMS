--menu_delete
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'menu_delete' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.menu_delete AS RAISERROR(''Procedure AddMenu is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE menu_delete
	    @id								INT 
AS

DELETE FROM Menu
WHERE id = @id;

GO

GRANT EXECUTE ON menu_delete TO sqlUser