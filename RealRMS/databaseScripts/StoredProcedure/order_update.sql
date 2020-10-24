IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'order_update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.order_update AS RAISERROR(''Procedure order_update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.order_update
	@numberOfSeats		INT,
	@delivered			BIT,
	@inProgress			BIT,
	@voided				BIT,
	@id					INT,
	@close				BIT = 0
AS

IF @close = 0
BEGIN
	UPDATE dbo.[order]
	SET NumberOfSeats = @numberOfSeats,
	Delivered = @delivered,
	InProgress = @inProgress,
	Voided = @voided/*,
	Closed = null*/
	WHERE id = @id
END ELSE BEGIN
	UPDATE dbo.[order]
	SET NumberOfSeats = @numberOfSeats,
	Delivered = @delivered,
	InProgress = @inProgress,
	Voided = @voided,
	Closed = CURRENT_TIMESTAMP
	WHERE id = @id
END

GO

GRANT EXECUTE ON order_update TO sqlUser

GO

/* exec order_update 4, 0, 0, 0, 9, 0 

select * from [order] */