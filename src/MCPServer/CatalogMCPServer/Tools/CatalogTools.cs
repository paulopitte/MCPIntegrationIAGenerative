using CatalogManagerAPI.Clients;
using CatalogMCPServer.Contracts.Resquests;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
namespace DotnetMCPServer.Tools;


[McpServerToolType]
public static class CatalogTools
{
    [McpServerTool, Description("Busca os produtos do catalogo, definindo um filtro opcional por título")]
    public static async Task<string> GetProducts(CatalogManagerHttpClient catalogApiClient,
        [Description("Filtro opcional pelo título do produto")] string title)
    {
        try
        {
            var products = await catalogApiClient.GetAllAsync(title);
            return products.Count == 0
                ? "Nenhum produto encontrado"
                : JsonSerializer.Serialize(products);
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao buscar produto: {ex.Message}";
        }
    }

    [McpServerTool, Description("Busca as categorias de produtos disponíveis para serem usadas no cadastro e alteração de produtos")]
    public static async Task<string> ObterCategoriasDeprodutos(CatalogManagerHttpClient catalogApiClient)
    {
        try
        {
            var categorias = await catalogApiClient.GetProductsByCategoryAsync();
            return categorias.Count == 0
                ? "Nenhuma categoria encontrada"
                : JsonSerializer.Serialize(categorias);
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao buscar categorias: {ex.Message}";
        }
    }

    [McpServerTool, Description("Busca um produto pelo código")]
    public static async Task<string> ObterprodutoPorId(CatalogManagerHttpClient catalogApiClient,
        [Description("Filtro obrigatório pelo id")] int id)
    {
        try
        {
            var produto = await catalogApiClient.GetProductByIdAsync(id);
            return produto is null
                ? "Nenhum produto encontrado"
                : JsonSerializer.Serialize(produto);
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao buscar produto: {ex.Message}";
        }
    }





    [McpServerTool, Description("Criar/Cadastrar um produto")]
    public static async Task<string> CadastrarProduto(CatalogManagerHttpClient catalogApiClient,
        [Description("Dados para criação do produto")] CreateProductRequest request)
    {
        try
        {
            var id = await catalogApiClient.CreateProductAsync(request);
            return id is null
                ? "Não foi possível cadastrar o produto"
                : JsonSerializer.Serialize(request);
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao cadastrar o produto: {ex.Message}";
        }
    }

    [McpServerTool, Description("Atualizar os dados de um produto")]
    public static async Task<string> AtualizarProduto(CatalogManagerHttpClient catalogApiClient,
        [Description("Código ou identificador do produto")] int id,
        [Description("Dados para atualização de um produto")] UpdateProductRequest request)
    {
        try
        {
            var sucesso = await catalogApiClient.UpdateProductAsync(id, request);
            return sucesso
                ? "produto atualizado com sucesso"
                : "Não foi possível atualizar o produto";
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao atualizar o produto: {ex.Message}";
        }
    }


    //[McpServerTool, Description("Atualizar apenas o preço de um produto")]
    //public static async Task<string> AtualizarPrecoDoproduto(CatalogManagerHttpClient catalogApiClient,
    //    [Description("Código ou identificador do produto")] int id,
    //    [Description("Novo preço do produto")] decimal preco)
    //{
    //    try
    //    {
    //        var sucesso = await produtoApiClient.AtualizarPrecoAsync(id, preco);
    //        return sucesso
    //            ? "Preço do produto atualizado com sucesso"
    //            : "Não foi possível atualizar o preço do produto";
    //    }
    //    catch (Exception ex)
    //    {
    //        //Log
    //        return $"Erro ao atualizar o preço do produto: {ex.Message}";
    //    }
    //}

    [McpServerTool, Description("Excluir um produto pelo código")]
    public static async Task<string> ExcluirProduto(CatalogManagerHttpClient catalogApiClient,
        [Description("Filtro obrigatório pelo id")] int id)
    {
        try
        {
            var produto = await catalogApiClient.DeleteProductAsync(id);
            return produto
                ? "produto excluído com sucesso"
                : "Erro ao excluir produto";
        }
        catch (Exception ex)
        {
            //Log
            return $"Erro ao excluir produto: {ex.Message}";
        }
    }
}

