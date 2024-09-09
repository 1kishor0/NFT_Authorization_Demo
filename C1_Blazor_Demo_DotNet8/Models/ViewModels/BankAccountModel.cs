using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace C1_Blazor_Demo_DotNet8.Models.ViewModels
{
    public class BankAccountModel
    {
        public string? AccountID { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Account Type is required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Initial Deposit is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Initial Deposit must be a positive number")]
        public decimal InitialDeposit { get; set; }

        public string NomineeFullName { get; set; }

        public string NomineeRelationship { get; set; }

        //public IBrowserFile IDProof { get; set; }
        public byte[] IDProofFile { get; set; }
    }
}
