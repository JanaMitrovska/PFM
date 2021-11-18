using PFMApi.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace PFMApi.Helpers
{
    public class CategoriesMappingModel : CsvMapping<Categories>
    {
        public CategoriesMappingModel()
       : base()
        {
            MapProperty(0, m => m.Code);
            MapProperty(1, m => m.ParentCode);
            MapProperty(2, m => m.Date);

        }
    }
}
