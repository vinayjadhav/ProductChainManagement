using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductChainManagement.ViewModels
{
    public class ClientRegistrationDisplayViewModel
    {
        public int Client_Id { get; set; }

        [Display(Name = "Customer Name")]
        public string Client_Name { get; set; }

        [Display(Name = "Address")]
        public string Client_Address { get; set; }

        [Display(Name = "City")]
        public string Client_City { get; set; }

        [Display(Name = "State")]
        public string Client_State { get; set; }

        [Display(Name = "Pincode")]
        public string Client_PinCode { get; set; }

        [Display(Name = "Contact No")]
        public string Client_PhoneNo { get; set; }

        [Display(Name = "Date of Birth")]
        //[DataType(DataType.DateTime)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "Gender")]
        public String Client_Gender { get; set; }

        [Display(Name = "Bank Name")]
        public string Bank_Name { get; set; }

        [Display(Name = "Branch")]
        public string Bank_Branch { get; set; }

        [Display(Name = "City")]
        public string Bank_City { get; set; }

        [Display(Name = "State")]
        public string Bank_State { get; set; }

        [Display(Name = "Pincode")]
        public string Bank_Pincode { get; set; }

        [Display(Name = "Account Type")]
        public String Bank_AccountType { get; set; }

        [Display(Name = "Account No.")]
        public string Bank_AccountNo { get; set; }

        [Display(Name = "IFSC")]
        public string Bank_IFSC { get; set; }

        [Display(Name = "Membership")]
        public String Membership_Type { get; set; }

        [Display(Name = "Customer Reference Code")]
        public string Ref_Cust_Code { get; set; }

        [Display(Name = "Your Customer Code")]
        public string Customer_code { get; set; }
    }
}