CREATE TABLE errors (
	id				INT PRIMARY KEY IDENTITY (1, 1),
	msg				VARCHAR(1000),
	stackTrace		VARCHAR(8000),
	url				VARCHAR(1000),
	urlReferer		VARCHAR(1000),
	request			VARCHAR(MAX),
	EmployeeId		INT,
	TimeOccurred		DATETIME2
)

CREATE TABLE requests (
	id				INT PRIMARY KEY IDENTITY(1, 1),
	urlReferrer		VARCHAR(1000),
	url				VARCHAR(1000),
	timeElapsed		FLOAT,
	EmployeeId		INT,
	TimeOccurred	DATETIME2
)


/*

select * from requests 

ALTER TABLE requests
ALTER COLUMN timeElapsed FLOAT

DROP TABLE errors
DROP TABLE requests
*/