using System.ComponentModel.DataAnnotations;

namespace TestTaskSigningOffer.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(12)]
        public string Bin { get; set; }
        public string Address { get; set; }
    }
}
