using AutoMapper;
using Microsoft.AspNetCore.Http;
using PFMApi.Data.Contracts;
using PFMApi.Data.Entitties;
using PFMApi.Helpers;
using PFMApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace PFMApi.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Categories> _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(IRepository<Categories> categoriesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<bool> AddCategories(HttpRequest request)
        {
            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, Encoding.UTF8);
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            CategoriesMappingModel csvMapper = new CategoriesMappingModel();

            CsvParser<Categories> csvParser = new CsvParser<Categories>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();

            result.Remove(result[result.Count - 1]);

            List<Categories> list = new List<Categories>();
            for (int i = 3; i < result.Count; i++)
            {
                Categories dataForDb = new Categories
                {
                    Code = result[i].Result.Code,
                    ParentCode = result[i].Result.ParentCode,
                    Date = result[i].Result.Date
                };
                list.Add(dataForDb);
            }

            await _categoriesRepository.AddRange(list);
            var resultFromRepo = await _categoriesRepository.SaveAll();

            if (resultFromRepo)
            {
                // primer za return i poraka
                //await $"Created Categories on server with name {dataForDb.Name}",
                //    "With method: AddCategories ", user, ip, browser);
                return true;
            }

            return false;
        }

        public async Task<Categories> getCategoryByCode(string code) {

            Categories toReturn = (Categories) _categoriesRepository.AsQueryable().Where(x => x.Code.Equals(code));
            return toReturn;
        }
    }
}
