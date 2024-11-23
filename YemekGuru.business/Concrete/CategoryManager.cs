using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository=categoryRepository;
    }

    public async Task<List<Category>> Admin_GetCategories30Async()
    {
        return await _categoryRepository.Admin_GetCategories30Async();
    }

    public async Task<List<Category>> Admin_GetCategoriesBySearchAsync(string search)
    {
        return await _categoryRepository.Admin_GetCategoriesBySearchAsync(search);
    }

    public async Task<bool> CreateAsync(Category entity)
    {
        return await _categoryRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Category entity)
    {
        return await _categoryRepository.DeleteAsync(entity);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetCategoriesAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetCategoryByIdAsync(id);
    }

    public async Task<int> TotalCategoriesNumberAsync()
    {
        return await _categoryRepository.TotalCategoriesNumberAsync();
    }

    public async Task<bool> UpdateAsync(Category entity)
    {
        return await _categoryRepository.UpdateAsync(entity);
    }
}
