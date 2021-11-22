using Microsoft.AspNetCore.Http;
using PFMApi.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Services.Contracts
{
    public interface ICategoriesService
    {
        Task<bool> AddCategories(HttpRequest request);
        Task<Categories> getCategoryByCode(string code);
    }
}
