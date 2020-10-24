IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'order_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.order_get AS RAISERROR(''Procedure order_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.order_get
	@id			INT
AS

SELECT id, numberofSeats, employeeId, tableId, delivered, inprogress
FROM [order]
WHERE id = @id;

GO

GRANT EXECUTE ON order_get TO sqlUser

GO
