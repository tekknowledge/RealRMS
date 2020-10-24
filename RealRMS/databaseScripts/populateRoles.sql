IF NOT EXISTS ( SELECT 1 FROM roles WHERE id = 1 )
BEGIN
	INSERT INTO roles (name, description)
	SELECT 'Restaurant Manager', ''
END

IF NOT EXISTS ( SELECT 1 FROM roles WHERE id = 2 )
BEGIN
	INSERT INTO roles (name, description)
	SELECT 'Server', ''
END

IF NOT EXISTS ( SELECT 1 FROM roles WHERE id = 3 )
BEGIN
	INSERT INTO roles (name, description)
	SELECT 'Runner', ''
END

IF NOT EXISTS ( SELECT 1 FROM roles WHERE id = 4 )
BEGIN
	INSERT INTO roles (name, description)
	SELECT 'Cook', ''
END