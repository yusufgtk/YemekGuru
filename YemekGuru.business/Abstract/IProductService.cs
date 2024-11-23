using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IProductService
{
    public Task<bool> CreateAsync(Product entity);
    public Task<bool> DeleteAsync(Product entity);
    public Task<bool> UpdateAsync(Product entity);
    public Task<List<Product>> GetAllAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task<Product> GetProductByNameAsync(string name);
    public Task<List<Product>> GetProductsAsync();
    public Task<List<Product>> GetProductsByRestaurantIdAsync(int restaurantId);
    public Task<List<Product>> GetTotalProductsByRestaurantIdAsync(int restaurantId);
    public Task<List<Product>> GetSellerProductsByRestaurantIdAsync(int restaurantId);
    public Task<List<Product>> GetProductsBySearchNameAsync(string searchName);
    public Task<List<Product>> GetProductsByLocationCategoryAsync(int? categoryId, string country, int? cityId, int? districtId);
    public Task<List<Product>> GetProductsByLocationAsync(string country, int? cityId, int? districtId);
    public Task<List<Product>> GetProductsByLocationSearchNameAsync(string? searchName, string country, int? cityId, int? districtId);
    public Task<List<Product>> Admin_GetProducts30Async();
    public Task<List<Product>> Admin_GetNotApprovedProducts30Async();
    public Task<List<Product>> Admin_GetProductsBySearchAsync(string search);
    public Task<List<Product>> Admin_GetNotApprovedProductsBySearchAsync(string search);
    public Task Admin_UpdateCategoryIdNullAsync(int categoryId);
    public Task<int> TotalProdutsNumberAsync();
    public Task<int> Admin_GetNotApprovedProductsNumberAsync();
}