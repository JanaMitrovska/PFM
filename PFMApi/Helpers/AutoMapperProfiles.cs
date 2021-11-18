using PFMApi.Data.Entities;
using PFMApi.Dto;
using AutoMapper;
using PFMApi.Data.Entitties;

namespace PFMApi.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Transactions, TransactionsDto>().ReverseMap();
			CreateMap<Categories, CategoriesDto>().ReverseMap();
		}
	}
}
