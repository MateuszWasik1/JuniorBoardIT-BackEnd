CREATE TABLE FavoriteJobOffers (
	FJOID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FJOGID uniqueidentifier NOT NULL,
	FJOUGID uniqueidentifier NOT NULL,
	FJOJOGID uniqueidentifier NOT NULL,
)