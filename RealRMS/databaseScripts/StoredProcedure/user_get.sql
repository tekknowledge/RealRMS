IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_get' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_get AS RAISERROR(''Procedure user_get is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_get
	@id  INT
/*
exec user_get 1 
*/
AS

SELECT id, firstName, lastName, middleName, [password], salt, dob, ssn, street,
city, [state], zip, phone, email, rate
FROM [user]
WHERE id = @id 

GO

GRANT EXECUTE ON user_get TO sqlUser

GO
