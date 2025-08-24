CREATE TABLE Applications (
	AID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AGID uniqueidentifier NOT NULL,
	AUGID uniqueidentifier NULL,
	AJOGID uniqueidentifier NOT NULL,
	AApplicationDate DATETIME2 NOT NULL,
)
