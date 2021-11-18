using PFMApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace PFMApi.Helpers
{
    public class TransactionsMappingModel : CsvMapping<Transactions>
    {
        public TransactionsMappingModel()
        : base()
        {
            MapProperty(0, m => m.Id);
            MapProperty(1, m => m.BenificaryName);
            MapProperty(2, m => m.Date);
            MapProperty(3, m => m.Direction);
            MapProperty(4, m => m.Amount);
            MapProperty(5, m => m.Description);
            MapProperty(6, m => m.Currency);
            MapProperty(7, m => m.Mcc);
            MapProperty(8, m => m.Kind);
        }
    }
}
