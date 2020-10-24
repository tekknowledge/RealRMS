-- =================================================
-- Create User as DBO template for SQL Azure Database
-- =================================================
-- For login <login_name, sysname, login_name>, create a user in the database
CREATE USER sqluser
	FOR LOGIN sqluser
	WITH DEFAULT_SCHEMA = dbo
GO

-- Add user to the database owner role
EXEC sp_addrolemember N'db_datawriter', N'sqluser'
GO
