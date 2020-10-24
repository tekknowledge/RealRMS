-- DROP TABLE [user]
DECLARE 
@webuserTable VARCHAR(100) = 'user'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @webuserTable )
BEGIN
	CREATE TABLE dbo.[user] (
		id			INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, --
		firstName	VARCHAR(100) NOT NULL,--
		lastName	VARCHAR(100) NOT NULL,--
		middleName	VARCHAR(100) NULL,--
		password	NVARCHAR(500) NULL,--
		salt		VARCHAR(100) NULL,--
		dob			DATE NOT NULL,--
		ssn			CHAR(11) NOT NULL,--
		street		VARCHAR(500) NOT NULL,--
		city		VARCHAR(50) NOT NULL,--
		state		VARCHAR(50) NOT NULL,--
		zip			CHAR(5) NOT NULL,--
		phone		CHAR(12) NOT NULL,
		email		VARCHAR(1000) NULL,
		rate		FLOAT	NOT NULL--
	);
END


DECLARE
@siteSettingsTable VARCHAR(100) = 'siteSettings'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables WHERE TABLE_NAME = @siteSettingsTable )
BEGIN
	CREATE TABLE siteSettings (
		id					INT IDENTITY(1, 1)  NOT NULL PRIMARY KEY,
		siteUrl				VARCHAR(1000) NOT NULL,
		databaseName		VARCHAR(1000) NOT NULL
	)
END

DECLARE
@siteSettings_whitelistedIps VARCHAR(100) = 'siteSettings_allowedIps'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables WHERE TABLE_NAME = @siteSettings_whitelistedIps )
BEGIN
	CREATE TABLE siteSettings_allowedIps (
		idSiteSetting		INT,
		ipAddress			VARCHAR(500)	
	)
END

DECLARE 
@roleTable VARCHAR(100) = 'roles'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @roleTable )
BEGIN
	CREATE TABLE dbo.roles (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		name	VARCHAR(100)
	);
END


DECLARE 
@userRoleTable VARCHAR(100) = 'user_roles'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @userRoleTable )
BEGIN
	CREATE TABLE dbo.user_roles (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		idRole	INT FOREIGN KEY REFERENCES roles(id) ON DELETE CASCADE,
		idUser	INT FOREIGN KEY REFERENCES [user](id) ON DELETE CASCADE
	);
END


DECLARE
@timeCardTable	VARCHAR(100) = 'timecard'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @timecardTable )
BEGIN
	CREATE TABLE dbo.timecard (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		Emp_Id	INT FOREIGN KEY REFERENCES [user](id) ON DELETE CASCADE,
		timeStarted	DATETIME2,
		timeEnded	DATETIME2,
		clockedIn AS (CASE WHEN timeEnded IS NULL THEN 
						CASE WHEN DATEDIFF(hh, timeStarted, timeEnded) < 12
						THEN CAST(1 AS BIT)
						ELSE CAST(0 AS BIT) END
					ELSE CAST(0 AS BIT) END)
	);
END

DECLARE /* Drop table menuCategory */
@menuCategory	VARCHAR(100) = 'menuCategory'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @menuCategory )
BEGIN
	CREATE TABLE dbo.menuCategory (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		name	VARCHAR(100),
		description	VARCHAR(100)
	)
	/* ALTER TABLE menuCategory
	   ADD name VARCHAR(100)
	*/
END

DECLARE /* Drop table menu */
@menuTable	VARCHAR(100) = 'menu'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @menuTable )
BEGIN
	CREATE TABLE dbo.menu (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		menuCategoryId	INT FOREIGN KEY REFERENCES menuCategory(id) ON DELETE CASCADE,
		name	VARCHAR(100),
		description	VARCHAR(1000),
		Price		FLOAT	NOT NULL--
	);
END

DECLARE /* Drop table menu */
@orderTable	VARCHAR(100) = 'order'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @orderTable )
BEGIN
	CREATE TABLE dbo.[order] (
		id	INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		NumberOfSeats		INT,
		EmployeeId			INT FOREIGN KEY REFERENCES [user](id),
		TableId				INT,
		Delivered			BIT,
		InProgress			BIT,
		Voided				BIT,
		Closed				DATETIME2
	);
END

DECLARE /* Drop table menu */
@orderItem	VARCHAR(100) = 'orderItem'
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = @orderItem )
BEGIN
	CREATE TABLE dbo.orderItem (
		id					INT PRIMARY KEY NOT NULL IDENTITY(1, 1),
		SeatNumber			INT,
		Ready				BIT,
		OrderId				INT FOREIGN KEY REFERENCES [order](id),
		MenuId				INT FOREIGN KEY REFERENCES menu(id),
		Comment				VARCHAR(1000)
	);
END
IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.tables where TABLE_NAME = 'Transaction' )
BEGIN
	CREATE TABLE dbo.[Transaction] (
		Transactionid			INT IDENTITY(1, 1)  PRIMARY KEY, --
		Orderid			        INT FOREIGN KEY REFERENCES [order](id),
		Method	                VARCHAR(255),
		CC_Number				VARCHAR(1000),
		CC_Confirmation			INT ,
		Change					FLOAT , 
		Tendered				FLOAT	,
		CC_salt					VARCHAR(1000) NULL,
		CC_Name					VARCHAR(1000) ,
		CC_Street				VARCHAR(1000) ,--
		CC_City					VARCHAR(1000) ,--
		CC_State				VARCHAR(100) ,--
		CC_Zip					CHAR(5) ,--
		Total 					FLOAT,	--
		Tip						FLOAT	--
	);
END