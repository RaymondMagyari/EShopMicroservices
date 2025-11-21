
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product? Product);

    internal class GetProductByIdQueryHandler (IDocumentSession session) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().Where(p => p.Id == request.ProductId).FirstOrDefaultAsync();

            if(product is null)
            {
                throw new ProductNotFoundException(request.ProductId);
            }

            return new GetProductByIdResult(product);
        }
    }
}
