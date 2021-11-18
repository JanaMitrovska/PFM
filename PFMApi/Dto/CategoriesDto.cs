using PFMApi.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace PFMApi.Dto
{
    public class CategoriesDto 
    {
        public string Code { get; set; }
        public string? ParentCode { get; set; }
        public string Date { get; set; }
    }
}
