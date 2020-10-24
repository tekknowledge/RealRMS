IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'order_copy' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.order_copy AS RAISERROR(''Procedure order_copy is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.order_copy
	@OrderIdToCopy		INT
AS

INSERT INTO [order] (NumberOfSeats, EmployeeId, TableId, Delivered, InProgress, Voided, Closed )
SELECT NumberOfSeats, EmployeeId, TableId, Delivered, InProgress, Voided, Closed FROM [order]
WHERE id = @OrderIdToCopy

SELECT SCOPE_IDENTITY();
GO

GRANT EXECUTE ON order_copy TO sqlUser

GO

/*

SELECT * FROM [order] WHERE tableId = 1 AND closed IS NULL

*/