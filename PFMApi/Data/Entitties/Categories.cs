using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Data.Entitties
{
    public class Categories
    {

        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string? ParentCode { get; set; }
        public string Date { get; set; }
    }
}
