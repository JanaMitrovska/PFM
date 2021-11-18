using PFMApi.Data.Contracts;
using PFMApi.Data.Entities;
using PFMApi.Dto;
using PFMApi.Helpers;
using PFMApi.Helpers.Params;
using PFMApi.Services.Contracts;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TinyCsvParser;
using System.IO;
using System.Text;

namespace PFMApi.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IRepository<Transactions> _transactionsRepository;
        private readonly IMapper _mapper;

        public TransactionsService(IRepository<Transactions> transactionsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _transactionsRepository = transactionsRepository;
        }


        public async Task<bool> AddTransactions(HttpRequest request)
        {

            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, Encoding.UTF8);
            var parsedDataString = await reader.ReadToEndAsync().ConfigureAwait(false);

            CsvReaderOptions csvReaderOptions = new CsvReaderOptions(new[] { "\n" });
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

            TransactionsMappingModel csvMapper = new TransactionsMappingModel();

            CsvParser<Transactions> csvParser = new CsvParser<Transactions>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromString(csvReaderOptions, parsedDataString)
                         .ToList();

            result.Remove(result[result.Count - 1]);

            List<Transactions> list = new List<Transactions>();
            for (int i = 3; i < result.Count; i++)
            {
                Transactions dataForDb = new Transactions
                {
                    Id = result[i].Result.Id,
                    BenificaryName = result[i].Result.BenificaryName,
                    Date = result[i].Result.Date,
                    Direction = result[i].Result.Direction,
                    Amount = result[i].Result.Amount,
                    Description = result[i].Result.Description,
                    Currency = result[i].Result.Currency,
                    Kind = result[i].Result.Kind,
                    Mcc = result[i].Result.Mcc
                };
                list.Add(dataForDb);
            }

          await _transactionsRepository.AddRange(list);
          var resultFromRepo = await _transactionsRepository.SaveAll();

            if (resultFromRepo)
            {
                // primer za return i poraka
                //await $"Created Transaction on server with name {dataForDb.Name}",
                //    "With method: AddTransactions ", user, ip, browser);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTransactions(int id)
        {
            var dataFromDb = await _transactionsRepository.GetById(id);
            if (dataFromDb != null)
            {
            
                _transactionsRepository.Delete(dataFromDb);
                var result = await _transactionsRepository.SaveAll();
                if (result)
                {
                    // primer za return i poraka
                    //await _loggerService.CreationLog($"Deleted Transaction with id: {id}",
                    //    "With method: DeleteTransactions ", user, ip, browser);
                    return true;
                }
            }

            return false;
        }
        public async Task<bool> UpdateTransactions(TransactionsDto transactionsDto)
        {
            var dataFromDb = await _transactionsRepository.GetById(transactionsDto.Id); // zemi go od baza preku ID

            if (dataFromDb != null)
            {

                dataFromDb.BenificaryName = transactionsDto.BenificaryName; 
                _transactionsRepository.Update(dataFromDb);

                if (await _transactionsRepository.SaveAll())
                {
                    //await _loggerService.CreationLog($"Updated Transaction with id: {transactionsDto.Id}",
                    //    "With method: UpdateTransactions ", user, ip, browser);
                    return true;
                }
            }

            return false;
        }
        public async Task<PagedList<Transactions>> GetPagedListTransactions(QueryParams transactionsParams)
        {
            var query = _transactionsRepository.AsQueryable();
            query = query.OrderBy(x => x.BenificaryName);

            // primer za filtriranje po Benificary - name
            if (!string.IsNullOrEmpty(transactionsParams.Name))
                query = query.Where(x => x.BenificaryName.Contains(transactionsParams.Name));

            return await PagedList<Transactions>.ToPagedList(query, transactionsParams.PageNumber, transactionsParams.PageSize);
        }
        public async Task<ICollection<TransactionsDto>> GetAllTransactions()
        {
            // bez paginacija

            //var dataFromDb = await _transactionsRepository.AsQueryable()
            //    .Where(x => x.DeletedBy == null && x.DeletedBy == null)
            //    .ToListAsync();

            return null; /*_mapper.Map<ICollection<TransactionsDto>>(dataFromDb);*/

        }
    }
}