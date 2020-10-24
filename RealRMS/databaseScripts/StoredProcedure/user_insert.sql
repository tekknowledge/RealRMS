IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'user_insert' AND ROUTINE_TYPE = 'procedure' AND ROUTINE_SCHEMA = 'dbo')
	EXEC sp_executesql N'CREATE PROCEDURE dbo.user_insert AS RAISERROR(''Procedure user_insert is incomplete.'', 16, 127);'
GO

ALTER PROCEDURE dbo.user_insert
	@firstName	VARCHAR(100),
	@lastName	VARCHAR(100),
	@middleName	VARCHAR(100) = null,
	@password	VARCHAR(500),
	@salt		VARCHAR(100),
	@dob		DATE,
	@ssn		CHAR(11),
	@street		VARCHAR(500),
	@city		VARCHAR(50),
	@state		VARCHAR(50),
	@zip		CHAR(5),
	@phone		CHAR(12),
	@email		VARCHAR(1000),
	@rate		FLOAT

	/* EXEC user_insert
	'Joe', 'Schmo', '', 'password', 'salt', '1/1/2015', 
	'', '', '', '', '', '', '', 7.95 */

AS

INSERT INTO [user] (firstName, lastName, middleName, [password], salt, dob, ssn,
				  street, city, [state], zip, phone, email, rate) 
SELECT @firstName, @lastName, @middleName, @password, @salt, @dob, @ssn,
				@street, @city, @state, @zip, @phone, @email, @rate

SELECT SCOPE_IDENTITY();

GO

GRANT EXECUTE ON user_insert TO sqlUser

GO