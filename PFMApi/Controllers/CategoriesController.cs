using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFMApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        // api/Categories/ImportCategories
        [HttpPost("[action]")]
        public async Task<IActionResult> ImportCategories()
        {
            var request = HttpContext.Request;
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            var status = await _categoriesService.AddCategories(request);
            if (status) return StatusCode(201);

            return BadRequest("Error while inserting the categories");
        }
    }
}
