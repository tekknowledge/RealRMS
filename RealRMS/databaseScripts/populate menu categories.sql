IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Appetizers' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Appetizers', 'Enjoy our most delectable bite size comfort food while you wait for your meal.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Soups' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Soups', 'Our soups are hearty and savory any time of year.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Salads' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Salads', 'Enjoy one of our salad selections made with fresh ingredients.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Subs/Sandwiches' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Subs/Sandwiches', 'We have a variety of hot and cold sandwiches.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Entrees' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Entrees', 'Enjoy one of our tasty entrees. Comes with a side.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Desserts' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Desserts', 'Don''t eat too much dinner. It''ll spoil your desert.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Beverages' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Beverages', 'Enjoy an ice cold refreshing beverage.'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Alcoholic Beverages' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Alcoholic Beverages', 'Kick back and relax with our tasty house specialties'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Breakfast' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Breakfast', 'Good old comfort breakfast foods'
END

IF NOT EXISTS ( SELECT 1 FROM menuCategory WHERE name = 'Sides' )
BEGIN
	INSERT INTO menuCategory (name, description)
	SELECT 'Sides', 'Order them with an entree or a-la-carte.'
END

select * from menuCategory
--update menuCategory set name = 'Breakfast' WHERE ID = 9