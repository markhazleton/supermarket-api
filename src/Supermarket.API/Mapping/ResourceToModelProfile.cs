using AutoMapper;
using Supermarket.API.Resources;
using Supermarket.Domain.Models;
using Supermarket.Domain.Models.Queries;

namespace Supermarket.API.Mapping
{
	public class ResourceToModelProfile : Profile
	{
		public ResourceToModelProfile()
		{
			CreateMap<SaveCategoryResource, Category>();

			CreateMap<SaveProductResource, Product>()
				.ForMember(src => src.UnitOfMeasurement, opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));

			CreateMap<ProductsQueryResource, ProductsQuery>();
		}
	}
}