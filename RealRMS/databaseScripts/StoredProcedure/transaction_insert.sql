IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'transaction_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.transaction_insert AS RAISERROR(''Procedure transaction_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.transaction_insert
	@OrderId		INT

AS

INSERT INTO [transaction] (orderId)
SELECT @OrderId

SELECT SCOPE_IDENTITY();

GO

GRANT EXECUTE ON transaction_insert TO sqlUser

GO