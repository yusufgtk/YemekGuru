using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductManager(IProductRepository productRepository)
    {
        _productRepository=productRepository;
    }

    public async Task<List<Product>> Admin_GetNotApprovedProducts30Async()
    {
        return await _productRepository.Admin_GetNotApprovedProducts30Async();
    }

    public async Task<List<Product>> Admin_GetNotApprovedProductsBySearchAsync(string search)
    {
        return await _productRepository.Admin_GetNotApprovedProductsBySearchAsync(search);
    }

    public async Task<int> Admin_GetNotApprovedProductsNumberAsync()
    {
        return await _productRepository.Admin_GetNotApprovedProductsNumberAsync();
    }

    public async Task<List<Product>> Admin_GetProducts30Async()
    {
        return await _productRepository.Admin_GetProducts30Async();
    }

    public async Task<List<Product>> Admin_GetProductsBySearchAsync(string search)
    {
        return await _productRepository.Admin_GetProductsBySearchAsync(search);
    }

    public async Task Admin_UpdateCategoryIdNullAsync(int categoryId)
    {
        await _productRepository.Admin_UpdateCategoryIdNullAsync(categoryId);
    }

    public async Task<bool> CreateAsync(Product entity)
    {
        return await _productRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Product entity)
    {
        return await _productRepository.DeleteAsync(entity);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
        return await _productRepository.GetProductByNameAsync(name);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _productRepository.GetProductsAsync();
    }

    public async Task<List<Product>> GetProductsByLocationAsync(string country, int? cityId, int? districtId)
    {
        return await _productRepository.GetProductsByLocationAsync(country="Turkey",cityId,districtId);
    }

    public async Task<List<Product>> GetProductsByLocationCategoryAsync(int? categoryId, string country, int? cityId, int? districtId)
    {
        return await _productRepository.GetProductsByLocationCategoryAsync(categoryId,country="Turkey",cityId,districtId);
    }

    public async Task<List<Product>> GetProductsByLocationSearchNameAsync(string? searchName, string country, int? cityId, int? districtId)
    {
        return await _productRepository.GetProductsByLocationSearchNameAsync(searchName,country="Turkey",cityId,districtId);
    }

    public async Task<List<Product>> GetProductsByRestaurantIdAsync(int restaurantId)
    {
        return await _productRepository.GetProductsByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Product>> GetProductsBySearchNameAsync(string searchName)
    {
        return await _productRepository.GetProductsBySearchNameAsync(searchName);
    }

    public async Task<List<Product>> GetSellerProductsByRestaurantIdAsync(int restaurantId)
    {
        return await _productRepository.GetSellerProductsByRestaurantIdAsync(restaurantId);
    }

    public async Task<List<Product>> GetTotalProductsByRestaurantIdAsync(int restaurantId)
    {
        return await _productRepository.GetTotalProductsByRestaurantIdAsync(restaurantId);
    }

    public Task<int> TotalProdutsNumberAsync()
    {
        return _productRepository.TotalProdutsNumberAsync();
    }

    public async Task<bool> UpdateAsync(Product entity)
    {
        return await _productRepository.UpdateAsync(entity);
    }
}
