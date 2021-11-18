
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
        private readonly IMapper _mapper;

        public TransactionController(ITransactionsService transactionsService, IMapper mapper)
        {
            _transactionsService = transactionsService;
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
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _transactionsService.DeleteTransactions(id);
            if (status) return Ok("Delete ok");

            return BadRequest("Error while deleting the entity");
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







