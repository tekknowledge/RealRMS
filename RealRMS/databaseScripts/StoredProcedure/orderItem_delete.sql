IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'orderItem_delete' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.orderItem_delete AS RAISERROR(''Procedure orderItem_delete is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.orderItem_delete
	@id			INT
AS

DELETE
FROM orderItem
WHERE id = @id

GO

GRANT EXECUTE ON orderItem_delete TO sqlUser

GO
