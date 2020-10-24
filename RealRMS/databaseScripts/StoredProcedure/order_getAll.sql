IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'order_getAll' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.order_getAll AS RAISERROR(''Procedure order_getAll is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.order_getAll
	@employeeId			INT = NULL
AS

IF @employeeId IS NULL
BEGIN
	SELECT id, numberofSeats, employeeId, tableId, delivered, inprogress
	FROM [order]
	WHERE isnull(voided, 0) != 1 AND closed is null
END ELSE BEGIN
	SELECT id, numberofSeats, employeeId, tableId, delivered, inprogress
	FROM [order]
	WHERE isnull(voided, 0) != 1  
	and closed is null
	AND employeeId = @employeeId
END

GO

GRANT EXECUTE ON order_getAll TO sqlUser

GO

/*
	select * from orderItem
*/