CREATE TABLE Reports (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RGID uniqueidentifier NOT NULL,
	RReporterGID uniqueidentifier NOT NULL,
	RSupportGID uniqueidentifier NULL,
	RDate DATETIME2 NOT NULL,
	RReasons nvarchar(8000) NOT NULL,
	RText nvarchar(8000) NOT NULL,
	RStatus INT NOT NULL,
)