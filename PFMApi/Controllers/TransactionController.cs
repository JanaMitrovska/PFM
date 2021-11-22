
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Fare;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFMApi.Data.Entities;
using PFMApi.Data.Entitties;
using PFMApi.Dto;
using PFMApi.Helpers;
using PFMApi.Helpers.Params;
using PFMApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;


namespace PFMApi.Controllers.ConfigurationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
       
        private readonly ITransactionsService _transactionsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionsService transactionsService, ICategoriesService categoriesService, IMapper mapper)
        {
            _transactionsService = transactionsService;
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        // api/transaction/ImportTransactions
        [HttpPost("[action]")]
        public async Task<IActionResult> ImportTransactions()
        {
            var request = HttpContext.Request;
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            var status = await _transactionsService.AddTransactions(request);
            if (status) return StatusCode(201);

            return BadRequest("Error while inserting the transactions");
        }

        // api/transaction/update
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(TransactionsDto transactionsDto)
        {
            var status = await _transactionsService.UpdateTransactions(transactionsDto);
            if (status) return StatusCode(200, transactionsDto);

            return BadRequest("Error while updating the entity");
        }

        // api/transaction/delete/{id}
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var status = await _transactionsService.DeleteTransactions(id);
            if (status) return Ok("Delete ok");

            return BadRequest("Error while deleting the entity");
        }

        // api/transaction/{id}/split
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Split([FromQuery] SplitParametars SpltParametars)
        {
            return Ok();
        }

        // api/transaction/{id}/categorize
        [HttpPost("[action]")]
        public async Task<IActionResult> Categorize([FromQuery] SplitParametars SplitParametars)
        {
            if(SplitParametars.TranscationId == null)
            {
                return BadRequest("Transaction Id invalid");
            } 
            else if (SplitParametars.CategoryId == null)
            {
                return BadRequest("Category Id invalid");
            }

            Task<Transactions> updatedTransaction = _transactionsService.GetTransactionById(SplitParametars.TranscationId);

            if (updatedTransaction == null)
            {
                return BadRequest("Transcation was not found in Database");
            }

           // if(await _categoriesService.getCategoryByCode(SplitParametars.CategoryId.ToString()) == null)
            //{
              //  return BadRequest("Category not found in Database");
            //}

                TransactionsDto transactionsDto = new TransactionsDto
            {
                Amount = (float)updatedTransaction.Result.Amount,
                BenificaryName = updatedTransaction.Result.BenificaryName,
                CategoryCode = SplitParametars.CategoryId,
                Currency = updatedTransaction.Result.Currency,
                Date = updatedTransaction.Result.Date,
                Description = updatedTransaction.Result.Description,
                Direction = updatedTransaction.Result.Direction,
                Id = updatedTransaction.Result.Id,
                Kind = updatedTransaction.Result.Kind,
                Mcc = updatedTransaction.Result.Mcc,
            };

            await _transactionsService.UpdateTransactions(transactionsDto);
            return Ok(transactionsDto);
        }

        // api/transaction/autocategorize
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> AutoCategorize(int TransactionId)
        {
            return Ok();
        }

        //  api/spending-analytics
        [HttpGet("[action]")]
        public async Task<IActionResult> SpendingAnalytics(string CategoryCode, string StartDate, string EndDate, string direction)
        {

            return Ok();
            /*
            // vrati lista na transakcii po kategorija, count i sum po kategorija
            spendings-by-category:
          type: object
          properties:
            groups:
              type: array
              description: List of spendings by category
              items:
                $ref: '#/components/schemas/spending-in-category'
        */
            /*
            spending-in-category:
              type: object
              properties:
                catcode:
                  type: string
                  description: Code of the category
                amount:
                  type: number
                  format: double
                  description: Amount spent in category
                count:
                  type: number
                  description: Number of transactions included in group
                  */
        }
        // api/transaction/GetTransactions/
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTransactions([FromQuery] QueryParams TransactionsParams)
        {
            var status = await _transactionsService.GetPagedListTransactions(TransactionsParams);

            Response.AddPagination(status.CurrentPage, status.PageSize, status.TotalCount, status.TotalPages);

            return Ok(_mapper.Map<List<TransactionsDto>>(status));
        }
    }
    }







