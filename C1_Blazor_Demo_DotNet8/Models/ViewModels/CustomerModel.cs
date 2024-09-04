using System.ComponentModel.DataAnnotations;

namespace C1_Blazor_Demo_DotNet8.Models.ViewModels
{
    public class CustomerModel
    {
        public int CustomerId { get; set; } = 0;
        public string? CustomerCategoryId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerShortName { get; set; }
        public string? CustomerSalutation { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerMiddleName { get; set; }
        public string? CustomerLastName { get; set; }
        public DateTime? MakeDt { get; set; }
        public string? Auth1stBy { get; set; }
        public DateTime? Auth1stDt { get; set; }
        public string? Auth2ndBy { get; set; }
        public DateTime? Auth2ndDt { get; set; }
        public string? AuthStatusId { get; set; }
        public string? LastAction { get; set; }

        [Required(ErrorMessage = "Make BY is required.")]
        public string? MakeBy { get; set; }
    }
}
