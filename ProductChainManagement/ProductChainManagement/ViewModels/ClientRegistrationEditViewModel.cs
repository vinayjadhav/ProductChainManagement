using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProductChainManagement.ViewModels
{
    public class ClientRegistrationEditViewModel
    {
        public int Client_Id { get; set; }

        [Required(ErrorMessage = "Please enter Customer Name")]
        [Display(Name = "Customer Name")]
        [StringLength(200, ErrorMessage = "Name cannot be more than 200 characters")]
        public string Client_Name { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        [StringLength(200, ErrorMessage = "Address cannot be more than 200 characters")]
        public string Client_Address { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "City cannot be more than 100 characters")]
        public string Client_City { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [Display(Name = "State")]
        [StringLength(100, ErrorMessage = "State cannot be more than 100 characters")]
        public string Client_State { get; set; }

        [Required(ErrorMessage = "Please enter Pincode")]
        [Display(Name = "Pincode")]
        [StringLength(6, ErrorMessage = "Pincode cannot be more than 6 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Characters not allowed")]
        public string Client_PinCode { get; set; }

        [Required(ErrorMessage = "Please enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Not a valid date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please enter Contact No.")]
        [Display(Name = "Contact No")]
        [StringLength(20, ErrorMessage = "Contact No. cannot be more than 20 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Characters not allowed")]
        public string Client_PhoneNo { get; set; }        

        [Required(ErrorMessage = "Please Select Gender")]
        [Display(Name = "Gender")]
        public int Client_Gender { get; set; }

        [Required(ErrorMessage = "Please enter Bank Name")]
        [Display(Name = "Bank Name")]
        [StringLength(100, ErrorMessage = "Bank name cannot be more than 100 characters")]
        public string Bank_Name { get; set; }

        [Required(ErrorMessage = "Please enter Branch")]
        [Display(Name = "Branch")]
        [StringLength(100, ErrorMessage = "Contact No. cannot be more than 100 characters")]
        public string Bank_Branch { get; set; }

        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "City cannot be more than 100 characters")]
        public string Bank_City { get; set; }

        [Display(Name = "State")]
        [StringLength(100, ErrorMessage = "State cannot be more than 100 characters")]
        public string Bank_State { get; set; }

        [Display(Name = "Pincode")]
        [StringLength(6, ErrorMessage = "Pincode cannot be more than 6 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Characters not allowed")]
        public string Bank_Pincode { get; set; }

        [Required(ErrorMessage = "Please enter Account Type")]
        [Display(Name = "Account Type")]
        public int Bank_AccountType { get; set; }

        [Required(ErrorMessage = "Please enter Account No.")]
        [Display(Name = "Account No.")]
        [StringLength(20, ErrorMessage = "Account No. cannot be more than 20 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Characters not allowed")]
        public string Bank_AccountNo { get; set; }

        [Required(ErrorMessage = "Please enter IFSC")]
        [Display(Name = "IFSC")]
        [StringLength(15, ErrorMessage = "IFSC cannot be more than 15 characters")]
        public string Bank_IFSC { get; set; }

        [Required(ErrorMessage = "Please enter select Membership")]
        [Display(Name = "Membership")]
        public int Membership_Type { get; set; }

        [Display(Name = "Customer Reference Code")]
        [StringLength(6, ErrorMessage = "Customer reference cannot be more than 6 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Characters not allowed")]
        public string Ref_Cust_Code { get; set; }

        [Display(Name = "Your Customer Code")]
        public string Customer_code { get; set; }
    }
}