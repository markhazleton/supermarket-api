using Supermarket.Domain.Models;

namespace Supermarket.Domain.Services.Communication
{
	public class ProductResponse : BaseResponse<Product>
	{
		public ProductResponse(Product product) : base(product) { }

		public ProductResponse(string message) : base(message) { }
	}
}