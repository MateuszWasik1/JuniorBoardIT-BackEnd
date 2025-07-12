CREATE TABLE [User] (
	UID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UGID uniqueidentifier NOT NULL,
	URID INT NOT NULL,
	UFirstName nvarchar(50) NOT NULL,
	ULastName nvarchar(50) NOT NULL,
	UUserName nvarchar(100) NOT NULL,
	UEmail nvarchar(100) NOT NULL,
	UPhone nvarchar(100) NOT NULL,
	UPassword nvarchar(max) NOT NULL,
);

CREATE TABLE AppRoles (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RGID uniqueidentifier NOT NULL,
	RName nvarchar(100) NOT NULL,
)

INSERT INTO AppRoles (RGID, RName) VALUES ('1A29FC40-CA47-1067-B31D-00DD0106621A', 'User'), ('2A29FC40-CA47-1067-B31D-00DD0106621A', 'Premium'), ('3C29FC40-CA47-1067-B31D-00DD0106623C', 'Recruiter'), ('4B29FC40-CA47-1067-B31D-00DD0106622B', 'Support'), ('5C29FC40-CA47-1067-B31D-00DD0106623C', 'Admin'); 

CREATE TABLE JobOffers (
	JOID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	JOGID uniqueidentifier NOT NULL,
    JORGID uniqueidentifier NOT NULL,
	JOTitle nvarchar(255) NOT NULL,
	JOCompanyName nvarchar(255) NOT NULL,
	JOLocationType int NOT NULL,
	JOOfficeLocation nvarchar(100) NULL,
	JOEmploymentType int NOT NULL,
	JOExpirenceLevel int NOT NULL,
    JOExpirenceYears decimal NOT NULL,
	JOCategory int NOT NULL,
	JOSalaryMin decimal NOT NULL,
	JOSalaryMax decimal NOT NULL,
	JOCurrency int NOT NULL,
	JOSalaryType int NOT NULL,
	JODescription nvarchar(2000) NOT NULL,
	JORequirements nvarchar(2000) NOT NULL,
	JOBenefits nvarchar(2000) NOT NULL,
	JOCreatedAt datetime2 NOT NULL,
	JOPostedAt datetime2 NOT NULL,
	JOExpiresAt datetime2 NOT NULL,
	JOStatus int NOT NULL,
)