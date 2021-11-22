using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Data.Entitties
{
    public class MccCodes
    {
        [Key]
        public int Code { get; set; }
        public string MerchanTtype { get; set; }
    }
}