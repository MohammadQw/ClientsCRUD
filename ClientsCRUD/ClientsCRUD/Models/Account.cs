using System.ComponentModel.DataAnnotations;

namespace ClientsCRUD.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }
        [Required]
        public string AccountType { get; set; }

    }
}
