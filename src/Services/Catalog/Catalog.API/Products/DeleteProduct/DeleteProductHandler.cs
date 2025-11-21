using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductValidatorCommand : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidatorCommand()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is requred");
        }
    }

    public class DeleteProductHandler(IDocumentSession session) :
        ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (productToDelete != null)
            {
                session.Delete(productToDelete);
                await session.SaveChangesAsync();

                return new DeleteProductResponse(true);
            }
            else
            {
                throw new ProductNotFoundException(request.Id);
            }
        }
    }
}
