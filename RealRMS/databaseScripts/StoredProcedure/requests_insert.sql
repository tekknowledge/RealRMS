IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'requests_insert'
 AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.requests_insert AS RAISERROR(''Procedure requests_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.requests_insert
	@urlReferrer		VARCHAR(1000),
	@url				VARCHAR(1000),
	@timeElapsed		FLOAT,
	@EmployeeId			int = null
AS

INSERT INTO requests (urlReferrer, url, timeElapsed, EmployeeId, TimeOccurred)
SELECT @urlReferrer, @url, @timeElapsed, @EmployeeId, CURRENT_TIMESTAMP

GO

GRANT EXECUTE ON requests_insert TO sqlUser

--select top 100 * from requests where timeOccurred > '5/5/2016 10:00:00 PM' order by timeOccurred desc
--SELECT * from errors

/*
select AVG(timeElapsed) from requests where CHARINDEX('.png', url) = 0 and CHARINDEX('.js', url) = 0 and CHARINDEX('.css', url) = 0 and CHARINDEX('.ico', url) = 0 and CHARINDEX('_browserlink', url) = 0
and timeOccurred > '5/5/2016 11:00:00 PM' 

select * from errors where timeOccurred > '5/5/2016 11:00:00 PM' 
*/
