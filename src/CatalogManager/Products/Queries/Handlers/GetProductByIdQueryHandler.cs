namespace CatalogManagerAPI.Products.Queries.Handlers;
public sealed class GetProductByIdQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product?> Handle(GetProductByIdQuery request)
    {
        return await _productRepository.GetProductByIdAsync(request.Id);
    }
}
