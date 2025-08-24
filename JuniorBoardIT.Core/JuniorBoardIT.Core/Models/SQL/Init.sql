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
	UCompanyGID uniqueidentifier NULL
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
    JOCGID uniqueidentifier NOT NULL,
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
	JOEducation int NOT NULL,
	JOCreatedAt datetime2 NOT NULL,
	JOPostedAt datetime2 NOT NULL,
	JOExpiresAt datetime2 NOT NULL,
	JOStatus int NOT NULL,
)

CREATE TABLE Reports (
	RID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	RGID uniqueidentifier NOT NULL,
	RJOGID uniqueidentifier NOT NULL,
	RReporterGID uniqueidentifier NULL,
	RSupportGID uniqueidentifier NULL,
	RDate DATETIME2 NOT NULL,
	RReasons nvarchar(4000) NOT NULL,
	RText nvarchar(4000) NOT NULL,
	RStatus INT NOT NULL,
)

CREATE TABLE Bugs (
	BID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	BGID uniqueidentifier NOT NULL,
	BUID INT NOT NULL,
	BAUIDS nvarchar(max) NULL,
	BDate DATETIME2 NOT NULL,
	BTitle nvarchar(200) NOT NULL,
	BText nvarchar(4000) NOT NULL,
	BStatus INT NOT NULL,
)

CREATE TABLE BugsNotes (
	BNID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	BNGID uniqueidentifier NOT NULL,
	BNBGID uniqueidentifier NOT NULL,
	BNUID INT NOT NULL,
	BNDate DATETIME2 NOT NULL,
	BNText nvarchar(4000) NOT NULL,
	BNIsNewVerifier BIT NOT NULL,
	BNIsStatusChange BIT NOT NULL,
	BNChangedStatus INT NOT NULL,
)

CREATE TABLE Companies (
	CID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CGID uniqueidentifier NOT NULL,
	CName nvarchar(255) NOT NULL,
	CIndustry int NOT NULL,
	CDescription nvarchar(2000) NOT NULL,
	CEmail nvarchar(255) NOT NULL,
	CAddress nvarchar(255) NOT NULL,
	CCity nvarchar(255) NOT NULL,
	CCountry nvarchar(255) NOT NULL,
	CPostalCode nvarchar(255) NOT NULL,
	CPhoneNumber nvarchar(255) NOT NULL,
	CNIP nvarchar(255) NOT NULL,
	CRegon nvarchar(255) NOT NULL,
	CKRS nvarchar(255) NOT NULL,
	CLI nvarchar(255) NOT NULL,
	CFoundedYear int NOT NULL,
	CEmployeesNo int NOT NULL,   
	CCreatedAt datetime2 NOT NULL,
	CUpdatedAt datetime2 NOT NULL,
)

CREATE TABLE FavoriteJobOffers (
	FJOID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FJOGID uniqueidentifier NOT NULL,
	FJOUGID uniqueidentifier NOT NULL,
	FJOJOGID uniqueidentifier NOT NULL,
)

CREATE TABLE Applications (
	AID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AGID uniqueidentifier NOT NULL,
	AUGID uniqueidentifier NULL,
	AJOGID uniqueidentifier NOT NULL,
	AApplicationDate DATETIME2 NOT NULL,
)
