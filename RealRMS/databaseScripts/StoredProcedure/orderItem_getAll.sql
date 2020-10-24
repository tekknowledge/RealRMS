IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'orderItem_getAll' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.orderItem_getAll AS RAISERROR(''Procedure orderItem_getAll is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.orderItem_getAll
	@orderid			INT
AS

SELECT id, comment, seatNumber, menuid, ready, orderid
FROM orderItem
WHERE orderId = @orderid

GO

GRANT EXECUTE ON orderItem_getAll TO sqlUser

GO
