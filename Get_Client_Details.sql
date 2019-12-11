Alter Procedure GetClientDetails
(
	@Client_id Int
)
AS
BEGIN

	Select CM.Client_Id,
	       CM.Name As Client_Name,
	       CM.Address As Client_Address,
		   CM.City As Client_City,
		   CM.State As Client_State,
		   CM.Pin_Code As Client_PinCode,
		   CM.Phone_No As Client_PhoneNo,
		   CM.DOB As DOB,
		   CM.Gender As Client_Gender,
		   BD.Bank_Name As Bank_Name,
		   BD.Branch As Bank_Branch,
		   BD.City As Bank_City,
		   BD.State As Bank_State,
		   BD.Pincode As  Bank_Pincode,
		   BD.Acc_Type As Bank_AccountType,
		   BD.Account_No As Bank_AccountNo,
		   BD.IFSC_Code As Bank_IFSC,
		   Membership_Type As Membership_Type,
		   (Select Customer_Code From ClientMaster Where Client_Id = CM.Ref_Client_Id) As Ref_Cust_Code,
		   CM.Customer_Code As Customer_Code
		   From ClientMaster CM Inner Join Bank_Details BD On CM.Client_Id = BD.Client_Id
		   Where CM.Client_Id = @Client_id
END

