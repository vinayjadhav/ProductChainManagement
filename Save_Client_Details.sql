Alter Procedure SaveClientDetails
(
    --Personal Details
	@Client_Id              INT,
	@Name                 VARCHAR(200),
	@Address              VARCHAR(200),
	@City                 VARCHAR(100),
	@State                VARCHAR(100),
	@Pin_Code             VARCHAR(10),
	@DOB                  DATE,
	@Gender               INT,
	@Phone_No             VARCHAR(20),
	@Ref_Customer_code    VARCHAR(10)=Null,
	@Membership_Type      INT,
	@Image_Path           VARCHAR(200) = NULL,
	@Signature_Path       VARCHAR(200) = NULL,
	--Bank Details
	@Bank_Name            VARCHAR(100),
	@Bank_Branch          VARCHAR(100),
	@Bank_City            VARCHAR(100) = NULL,
	@Bank_State           VARCHAR(100) = NULL,
	@Bank_Pincode         VARCHAR(10) = NULL,
	@Accout_Type          INT,
	@Account_No           VARCHAR(100),
	@IFSC_Code            VARCHAR(50)
)
AS
BEGIN
	Declare @Ref_Client_Id INT,
	        @Customer_Code VARCHAR(10),
			@First_Digit INT,
			@Stop_Looping BIT = 1,
			@New_Client_Id Int
			

	Select @Ref_Client_Id = Client_Id From ClientMaster Where Customer_Code = LTRIM(RTRIM(@Ref_Customer_code))

	IF (@Ref_Customer_code IS NOT NULL AND @Ref_Client_Id IS NULL)
	BEGIN
		RAISERROR('Incorrect customer reference code entered',16,1)
		RETURN
	END

	IF(@Client_Id > 0)
	BEGIN
		UPDATE ClientMaster
		   SET Name=@Name,
		       Address=@Address,
		       City=@City,
               State=@State,
               Pin_Code=@Pin_Code,
               DOB=@DOB,
               Gender=@Gender,
               Phone_No=@Phone_No,
               Ref_Client_Id=@Ref_Client_Id,
               Membership_Type=@Membership_Type,
               DateTimeStamp=GETDATE()
	     WHERE Client_Id = @Client_Id

		 UPDATE Bank_Details
		    SET Bank_Name = @Bank_Name,
			    Branch = @Bank_Branch,
				City = @Bank_City,
				State = @Bank_State,
                Pincode = @Bank_Pincode,
                Acc_Type = @Accout_Type,
                Account_No = @Account_No,
                IFSC_Code = @IFSC_Code
		 WHERE Client_Id = @Client_Id

		 Exec ViewClientDetails @Client_Id

		 return 0
	END
 
	WHILE(1=1)
	BEGIN
		Set @First_Digit = FLOOR(RAND()*10)

		If @First_Digit = 0
			SET @First_Digit  = 1

		SET @Customer_Code = Convert(VARCHAR,@First_Digit) + 
							 Convert(VARCHAR,FLOOR(RAND()*10)) + 
							 Convert(VARCHAR,FLOOR(RAND()*10)) + 
							 Convert(VARCHAR,FLOOR(RAND()*10)) + 
							 Convert(VARCHAR,FLOOR(RAND()*10)) +
							 Convert(VARCHAR,FLOOR(RAND()*10))

		IF NOT EXISTS(Select 1 from ClientMaster Where Customer_Code = @Customer_Code)
			BREAK
	END

	Insert Into ClientMaster
	(
		Customer_Code,
		Name,
		Address,
		City,
		State,
		Pin_Code,
		DOB,
		Gender,
		Phone_No,
		Ref_Client_Id,
		Membership_Type,
		Image_Path,
		Signature_Path,
		DateTimeStamp,
		Can_Login,
		Active
	)
	VALUES
	(
		@Customer_Code,
		@Name,
		@Address,
		@City,
		@State,
		@Pin_Code,
		@DOB,
		@Gender,
		@Phone_No,
		@Ref_Client_Id,
		@Membership_Type,
		@Image_Path,
		@Signature_Path,
		GETDATE(),
		0,
		1
	)

	IF (@Client_Id > 0)
		Set @New_Client_Id = @Client_Id
	Else
		Set @New_Client_Id = @@IDENTITY

	Insert Into Bank_Details
	(
		Client_Id,
		Bank_Name,
		Branch,
		City,
		State,
		Pincode,
		Acc_Type,
		Account_No,
		IFSC_Code
	)
	Values
	(
		@New_Client_Id,
		@Bank_Name,
		@Bank_Branch,
		@Bank_City,
		@Bank_State,
		@Bank_Pincode,
		@Accout_Type,
		@Account_No,
		@IFSC_Code
	)

	--To give back the newly generated Client's data
	Exec ViewClientDetails @New_Client_Id
END

--Unit Testing
/*
Exec SaveClientDetails @Name           =      'Vinay Jadhav',
	@Address              ='701,E-3,Royal Park',
	@City                 ='Ambernath',
	@State                ='Maharashtra',
	@Pin_Code             ='421501',
	@DOB                  ='1983-10-08',
	@Gender               =1,
	@Phone_No             =9619299983,
	@Ref_Customer_code    =Null,
	@Membership_Type      1,
	@Image_Path            = NULL,
	@Signature_Path       = NULL,
	--Bank Details
	@Bank_Name            ='HDFC Bank',
	@Bank_Branch          ='Vashi',
	@Bank_City            ='Navi-Mumbai',
	@Bank_State           ='Maharashtra',
	@Bank_Pincode         ='421501',
	@Accout_Type          =1,
	@Account_No           ='16545165421',
	@IFSC_Code            ='HDFC00042'
*/