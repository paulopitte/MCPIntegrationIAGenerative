using CatalogMCPServer.Contracts.Responses;
using CatalogMCPServer.Contracts.Resquests;
using System.Net.Http.Json;
using System.Text.Json;


namespace CatalogManagerAPI.Clients;
public class CatalogManagerHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public CatalogManagerHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<ProductResponse>> GetAllAsync(string? titulo = null)
    {
        var url = string.IsNullOrWhiteSpace(titulo) ? "products" : $"products?title={Uri.EscapeDataString(titulo)}";
        var response = await _httpClient.GetAsync(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return new List<ProductResponse>();

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ProductResponse>>(_jsonOptions);
    }

    public async Task<List<CategoryResponse>> GetProductsByCategoryAsync()
    {
        var url = "category";
        var response = await _httpClient.GetAsync(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return new List<CategoryResponse>();

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CategoryResponse>>(_jsonOptions);
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductResponse>(_jsonOptions);
    }

    public async Task<ProductResponse?> CreateProductAsync(CreateProductRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("products", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var product = await response.Content.ReadFromJsonAsync<ProductResponse>(_jsonOptions);
        return product;
    }

    public async Task<bool> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"products/{id}", request);
        return response.IsSuccessStatusCode;
    }

    //public async Task<bool> AtualizarPrecoAsync(int id, decimal novoPreco)
    //{
    //    var response = await _httpClient.PatchAsync($"products/{id}/preco/{novoPreco}", null);
    //    return response.IsSuccessStatusCode;
    //}

    public async Task<bool> DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"products/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        return true;
    }
}


