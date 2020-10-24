IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'orderItem_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.orderItem_insert AS RAISERROR(''Procedure orderItem_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.orderItem_insert
	@comment	VARCHAR(1000) = NULL,
	@seatNumber	INT,
	@orderId	INT,
	@menuId		INT
AS

INSERT INTO orderItem (comment, seatNumber, ready, orderId, menuId) 
SELECT @comment, @seatNumber, 0, @orderId, @menuId

SELECT SCOPE_IDENTITY();

GO

GRANT EXECUTE ON orderItem_insert TO sqlUser

GO

/* select * from orderItem
truncate table orderItem
 */
