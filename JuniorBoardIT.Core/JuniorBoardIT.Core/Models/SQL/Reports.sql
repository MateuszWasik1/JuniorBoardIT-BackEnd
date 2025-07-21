CREATE TABLE Reports (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RGID uniqueidentifier NOT NULL,
	RJOGID uniqueidentifier NOT NULL,
	RReporterGID uniqueidentifier NOT NULL,
	RSupportGID uniqueidentifier NULL,
	RDate DATETIME2 NOT NULL,
	RReasons nvarchar(4000) NOT NULL,
	RText nvarchar(4000) NOT NULL,
	RStatus INT NOT NULL,
)