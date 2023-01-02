using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskSigningOffer.Models
{
    public class Contract
    {
        public int Id { get; set; }
        [Required]
        public int IdCompany { get; set; }
        [Required]
        public string FullName { get; set; }
        [StringLength(12)]
        public string Iin { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Sms { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool SignSmsClient { get; set; }
        public bool SignSmsCompany { get; set; }
    }
}
