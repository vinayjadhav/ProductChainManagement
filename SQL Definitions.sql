
Create Table Membership_Type
(
	Id            INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Description   VARCHAR(50)
)

GO

INSERT INTO Membership_Type (Description) VALUES ('Customer')
INSERT INTO Membership_Type (Description) VALUES ('Member')

GO

Create Table Gender
(
	Id            INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Description   VARCHAR(20)
)

GO

INSERT INTO Gender (Description) VALUES ('Male')
INSERT INTO Gender (Description) VALUES ('Female')
INSERT INTO Gender (Description) VALUES ('Transgender')

GO

Create Table ClientMaster
(
	Client_Id        INT IDENTITY(1,1) PRIMARY KEY,
	Customer_Code    VARCHAR(10),
	Password         VARCHAR(100),
	Name             VARCHAR(200),
	Address          VARCHAR(200),
	City             VARCHAR(100),
	State            VARCHAR(100),
	Pin_Code         VARCHAR(10),
	DOB              DATE,
	Gender           INT FOREIGN KEY REFERENCES Gender(Id),
	Phone_No         VARCHAR(20),
	Ref_Client_Id    INT,
	Membership_Type  INT FOREIGN KEY REFERENCES Membership_Type(Id),
	Image_Path       VARCHAR(200),
	Signature_Path   VARCHAR(200),
	DateTimeStamp    DATETIME,
	Can_Login        BIT,
	Active           BIT
)

GO

Create Table Account_Type
(
	Id           INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Description  VARCHAR(20)
)

GO

INSERT INTO Account_Type (Description) VALUES ('Savings')
INSERT INTO Account_Type (Description) VALUES ('Current')

GO

Create Table Bank_Details
(
	Bank_Details_Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Client_Id       INT FOREIGN KEY REFERENCES ClientMaster(Client_Id),
	Bank_Name       VARCHAR(100),
	Branch          VARCHAR(100),
	City            VARCHAR(100),
	State           VARCHAR(100),
	Pincode         VARCHAR(10),
	Acc_Type        INT FOREIGN KEY REFERENCES Account_Type(Id),
	Account_No      VARCHAR(100),
	IFSC_Code       VARCHAR(50)
)

GO

Create Table Payment_Mode
(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Description VARCHAR(20)
)

GO

INSERT INTO Payment_Mode (Description) VALUES ('Cash')
INSERT INTO Payment_Mode (Description) VALUES ('Cheque')
INSERT INTO Payment_Mode (Description) VALUES ('GPay')
INSERT INTO Payment_Mode (Description) VALUES ('PhonePe')
INSERT INTO Payment_Mode (Description) VALUES ('Bank Transfer')
INSERT INTO Payment_Mode (Description) VALUES ('Credit Card')
INSERT INTO Payment_Mode (Description) VALUES ('Debit Card')

GO

Create Table Client_Payment_Details
(
	Client_Id         INT FOREIGN KEY REFERENCES ClientMaster(Client_Id),
	Payment_Mode      INT FOREIGN KEY REFERENCES Payment_Mode(Id),
	Transaction_No    VARCHAR(100),
	Transaction_Date  DATE
)
