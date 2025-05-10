namespace CatalogManagerAPI.Products;
public interface ICommandHandler<TCommand> where TCommand : class
{
    Task Handle(TCommand command);
}
