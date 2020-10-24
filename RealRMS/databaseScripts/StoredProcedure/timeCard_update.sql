--timeCard_Update
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'timeCard_Update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.timeCard_Update AS RAISERROR(''Procedure timeCard_Update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.timeCard_update
	@EmployeeId			INT = null,
	@TimeStarted		datetime2 = null,
	@Id					INT = null
AS

IF ( @EmployeeId IS NULL AND @TimeStarted IS NULL ) AND (@Id IS NULL)
BEGIN
	RETURN;
END
IF @Id IS NOT NULL
BEGIN
	UPDATE timeCard
	SET timeEnded = CURRENT_TIMESTAMP
	WHERE Id = @Id;
END ELSE
BEGIN
	UPDATE timeCard
	SET timeEnded = CURRENT_TIMESTAMP
	WHERE Emp_Id = @EmployeeId AND timeStarted = @TimeStarted;
END
GO

GRANT EXECUTE ON timeCard_Update TO sqlUser