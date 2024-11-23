using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface ICategoryRepository:IRepository<Category>
{
    public Task<Category> GetCategoryByIdAsync(int id);
    public Task<List<Category>> GetCategoriesAsync();
    public Task<List<Category>> Admin_GetCategories30Async();
    public Task<List<Category>> Admin_GetCategoriesBySearchAsync(string search);
    public Task<int> TotalCategoriesNumberAsync();
}