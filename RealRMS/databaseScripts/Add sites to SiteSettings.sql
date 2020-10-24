
IF NOT EXISTS ( SELECT 1 FROM siteSettings WHERE siteUrl = 'thebestrestaurant.com' )
BEGIN
	INSERT INTO siteSettings 
	SELECT NULL, 'thebestrestaurant.com', 'RMS.Restaurant1'
END