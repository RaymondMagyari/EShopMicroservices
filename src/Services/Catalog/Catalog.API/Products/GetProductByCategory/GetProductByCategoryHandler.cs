using Catalog.API.Exceptions;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryHandler (IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var results = await session.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync();

            if(results.Any())
            {
                return new GetProductByCategoryResult(results);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
