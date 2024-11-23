using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface ICategoryService
{
    public Task<bool> CreateAsync(Category entity);
    public Task<bool> DeleteAsync(Category entity);
    public Task<bool> UpdateAsync(Category entity);
    public Task<List<Category>> GetAllAsync();
    public Task<Category> GetCategoryByIdAsync(int id);
    public Task<List<Category>> GetCategoriesAsync();
    public Task<List<Category>> Admin_GetCategories30Async();
    public Task<List<Category>> Admin_GetCategoriesBySearchAsync(string search);
    public Task<int> TotalCategoriesNumberAsync();
}