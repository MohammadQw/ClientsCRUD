using ClientsCRUD.Models;
using ClientsCRUD.Validators;
using System.ComponentModel.DataAnnotations;

namespace ClientsCRUD.DTOs
{
    public class CreateClientDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PersonalId { get; set; }

        public string ProfilePhoto { get; set; }

        [Required]
        [PhoneNumber]
        public string MobileNumber { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public AddressDto Address { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one account is required.")]
        public List<AccountDto> Accounts { get; set; }
    }

    public class AddressDto
    {

        public int ClientId { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }

    public class AccountDto
    {
      
        public int ClientId { get; set; }
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        public string AccountType { get; set; }
    }

    public class ClientQueryParameters
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SortBy { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
    public class UpdateClientDto : CreateClientDto
    {
        
    }
    public class ClientDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string ProfilePhoto { get; set; }
        public string MobileNumber { get; set; }
        public string Sex { get; set; }
        public AddressDto Address { get; set; }
        public List<AccountDto> Accounts { get; set; }
    }

}
