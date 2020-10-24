IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'order_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.order_insert AS RAISERROR(''Procedure order_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.order_insert
	@numberOfSeats		INT,
	@employeeId			INT,
	@tableId			INT
AS

IF NOT EXISTS ( SELECT 1 FROM [order] WHERE tableId = @tableId AND closed IS NULL )
BEGIN
	INSERT INTO [order] (numberofseats, employeeId, tableid)
	SELECT @numberOfSeats, @employeeId, @tableId

	SELECT SCOPE_IDENTITY();
END
GO

GRANT EXECUTE ON order_insert TO sqlUser

GO

/*

SELECT 1 FROM [order] WHERE tableId = 1 AND closed IS NULL

*/