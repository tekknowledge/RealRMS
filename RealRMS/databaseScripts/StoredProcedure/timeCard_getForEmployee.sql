--timecard_getForEmployee
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'timecard_getForEmployee' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.timecard_getForEmployee AS RAISERROR(''Procedure timecard_getForEmployee is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.timecard_getForEmployee
	@EmployeeId			INT
AS
/* EXEC timecard_getForEmployee 1 */

SELECT TOP 20 
Id, Emp_Id, timeStarted AS [In], timeEnded AS [Out]
FROM timeCard
WHERE Emp_Id = @EmployeeId
ORDER BY Id desc

GO

GRANT EXECUTE ON timecard_getForEmployee TO sqlUser