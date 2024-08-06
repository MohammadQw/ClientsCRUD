
using ClientsCRUD.Validators;
using System.ComponentModel.DataAnnotations;

namespace ClientsCRUD.Models
{
    public class Client
    {
        public int Id { get; set; }

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
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; }

        [Required]
        public Address Address { get; set; } = new Address();

        [Required]
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
    public enum Sex
    {
        Male,
        Female
    }
}

