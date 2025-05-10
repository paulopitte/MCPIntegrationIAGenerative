﻿namespace CatalogManagerAPI.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> GetAllProductsAsync(string? title)
    {
        return await _context.Products
            .AsNoTrackingWithIdentityResolution()
            .Include(p => p.Category)
            .Where(p => string.IsNullOrWhiteSpace(title) || p.Name.Contains(title))           
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(p => p.Id == id);

        return product ?? throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
