using System.ComponentModel.DataAnnotations;

namespace ClientsCRUD.Models
{
    public class Address
    {
        public int Id { get; set; }
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
}
