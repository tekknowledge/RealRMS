IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'orderItem_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.orderItem_get AS RAISERROR(''Procedure orderItem_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.orderItem_get
	@id			INT
AS

SELECT id, comment, seatNumber, menuid, ready, orderid
FROM orderItem
WHERE id = @id

GO

GRANT EXECUTE ON orderItem_get TO sqlUser

GO
