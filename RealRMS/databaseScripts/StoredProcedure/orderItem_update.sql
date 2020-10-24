IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'orderItem_update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.orderItem_update AS RAISERROR(''Procedure orderItem_update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.orderItem_update
	@id			INT,
	@comment	VARCHAR(1000) = NULL,
	@seatNumber	INT,
	@menuId		INT,
	@ready		BIT = NULL
AS

IF @Ready IS NULL
BEGIN
	UPDATE orderItem
	set comment = @comment,
	seatNumber = @seatNumber,
	menuId = @menuId
	WHERE id = @id;
END ELSE BEGIN
	UPDATE orderItem
	set comment = @comment,
	seatNumber = @seatNumber,
	menuId = @menuId,
	ready = @ready
	WHERE id = @id;
END

GO

GRANT EXECUTE ON orderItem_update TO sqlUser

GO
/* select * from [order] */
