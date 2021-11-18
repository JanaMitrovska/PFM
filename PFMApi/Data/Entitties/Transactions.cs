using PFMApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PFMApi.Data.Entities
{   
    public class Transactions
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(255)]
        public string? BenificaryName { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Direction { get; set; }
        [Required]
        public double Amount { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required, MaxLength(3), MinLength(3)]
        public string Currency { get; set; }    
        public int? Mcc { get; set; }
        [Required]
        public string Kind { get; set; }
}
}
