IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'transaction_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.transaction_get AS RAISERROR(''Procedure transaction_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.transaction_get
	@Id		INT

AS

SELECT TransactionId as Id, OrderId
FROM [transaction]
WHERE Id = @id

GO

GRANT EXECUTE ON transaction_get TO sqlUser

GO