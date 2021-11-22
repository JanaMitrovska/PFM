using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Dto
{
    public class TransactionsDto
    {
        public string Id { get; set; }
        public string? BenificaryName { get; set; }
        public string Date { get; set; }
        public string Direction { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public int? Mcc { get; set; }
        public string Kind { get; set; }
        public string CategoryCode { get; set; }

    }
}
