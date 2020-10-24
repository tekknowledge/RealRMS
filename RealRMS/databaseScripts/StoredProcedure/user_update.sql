IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_update' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_update AS RAISERROR(''Procedure user_update is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_update
	@id			INT,
	@firstName	VARCHAR(100),
	@lastName	VARCHAR(100),
	@middleName	VARCHAR(100) = null,
	@dob		DATE,
	@ssn		CHAR(11),
	@street		VARCHAR(500),
	@city		VARCHAR(50),
	@state		VARCHAR(50),
	@zip		CHAR(5),
	@phone		CHAR(12),
	@email		VARCHAR(1000),
	@rate		FLOAT

AS

UPDATE [user]
SET firstName = @firstName, lastName = @lastName, middleName = @middleName, dob = @dob, 
ssn = @ssn, street = @street, city = @city, [state] = @state, zip = @zip, phone = @phone, 
email = @email, rate = @rate
WHERE Id = @id

GO

GRANT EXECUTE ON user_update TO sqlUser

GO