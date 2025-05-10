using System.Text.Json.Serialization; 

public record CreateProductRequest
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public bool Active { get; set; } = true;
    [JsonIgnore]
    public int Id { get; internal set; } // Adicionado para o CreatedAtAction
}


