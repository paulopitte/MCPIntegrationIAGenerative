namespace CatalogMCPServer.Contracts.Responses;

// NAO CONSIDERO O USO DO MODELO DE ENTIDADE (DDD)
public record class ProductResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool Active { get; set; } = true;

    public int CategoryId { get; set; }
    public CategoryResponse? Category { get; set; }
}
