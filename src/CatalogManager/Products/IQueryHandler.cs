namespace CatalogManagerAPI.Products;
public interface IQueryHandler<TQuery, TResult> where TQuery : class
{
    Task<TResult> Handle(TQuery query);
}
