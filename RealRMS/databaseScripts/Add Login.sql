-- =================================================
-- Create User as DBO template for SQL Azure Database
-- =================================================
USE master

CREATE LOGIN sqluser WITH PASSWORD = 'd33zNutz'

CREATE USER sqluser
	FOR LOGIN sqluser
	WITH DEFAULT_SCHEMA = dbo
GO
