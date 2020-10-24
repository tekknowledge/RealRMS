IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_getAll' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_getAll AS RAISERROR(''Procedure user_getAll is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_getAll
AS

SELECT id, firstName, lastName, middleName, [password], salt, dob, ssn, street,
city, [state], zip, phone, email, rate
FROM [user]

GO

GRANT EXECUTE ON user_getAll TO sqlUser

GO
