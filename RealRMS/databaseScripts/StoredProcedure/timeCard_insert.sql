--timeCard_Insert
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'timeCard_Insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.timeCard_Insert AS RAISERROR(''Procedure timeCard_Insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.timeCard_Insert
	@EmployeeId			INT
AS

INSERT INTO timeCard (Emp_Id, timeStarted, timeEnded)
SELECT @EmployeeId, CURRENT_TIMESTAMP, NULL

SELECT SCOPE_IDENTITY()

GO

GRANT EXECUTE ON timeCard_Insert TO sqlUser

/* 
	select * from timeCard
	truncate table timecard
	exec timeCard_insert 24
*/