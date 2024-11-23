using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class CategoryRepository : Repository<Category, DataContext>, ICategoryRepository
{
    public async Task<List<Category>> Admin_GetCategories30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Categories.OrderBy(i=>i.UpdatedAt).Take(30).ToListAsync();
        }
    }

    public async Task<List<Category>> Admin_GetCategoriesBySearchAsync(string search)
    {
        using(var context = new DataContext())
        {
            int categoryId=-1;
            try{
                categoryId=Convert.ToInt32(search);
            }
            catch(Exception err)
            {
                categoryId=-1;
                
            }
            return await context.Categories.Where(i=>i.Id==categoryId||i.Name.ToLower().Contains(search.ToLower())).ToListAsync();
        }
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Categories.ToListAsync();
        }
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.Categories.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<int> TotalCategoriesNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Categories.CountAsync();
        }
    }
}
