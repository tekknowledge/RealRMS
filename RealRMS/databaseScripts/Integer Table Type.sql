IF NOT EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = 'MyType')
BEGIN
	CREATE TYPE dbo.IntegerList
	AS TABLE
	(
	  Value INT
	);
END
GO
GRANT EXEC ON TYPE::[dbo].[IntegerList] TO sqlUser
GO