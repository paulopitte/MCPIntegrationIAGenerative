namespace CatalogMCPServer.Contracts.Responses;
public record class CategoryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<ProductResponse>? Products { get; set; }
}
