using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class ProductRepository : Repository<Product, DataContext>, IProductRepository
{
    public async Task<Product> GetProductByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.Products.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
        using(var context = new DataContext())
        {
            return await context.Products.FirstOrDefaultAsync(i=>i.Name==name);
        }
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Products.ToListAsync();
        }
    }

    public async Task<List<Product>> GetProductsByLocationAsync(string country, int? cityId, int? districtId)
    {
        using(var context = new DataContext())
        {
            return await context.Products
                                        .Include(p => p.Restaurant)  // Ürünlerin restoran bilgilerini de dahil et
                                        .Where(p => p.Restaurant.Country == country
                                                    && p.Restaurant.CityId == cityId
                                                    && p.Restaurant.DistrictId == districtId
                                                    &&p.IsActive==true).ToListAsync();
        }
    }

    public async Task<List<Product>> GetProductsByLocationCategoryAsync(int? categoryId, string country, int? cityId, int? districtId)
    {
        using(var context = new DataContext())
        {
            return await context.Products
                                        .Include(p => p.Restaurant)  // Ürünlerin restoran bilgilerini de dahil et
                                        .Where(p => p.Restaurant.Country == country
                                                    && p.Restaurant.CityId == cityId
                                                    && p.Restaurant.DistrictId == districtId
                                                    && p.CategoryId == categoryId
                                                    &&p.IsActive==true)
                                        .ToListAsync();
        }
        
    }

    public async Task<List<Product>> GetProductsByLocationSearchNameAsync(string? searchName, string country, int? cityId, int? districtId)
    {
        using(var context = new DataContext())
        {
            return await context.Products.Include(p => p.Restaurant)  // Ürünlerin restoran bilgilerini de dahil et
                                        .Where(p => p.Restaurant.Country == country
                                                && p.Restaurant.CityId == cityId
                                                && p.Restaurant.DistrictId == districtId
                                                &&p.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();
        }
    }

    public async Task<List<Product>> GetProductsByRestaurantIdAsync(int restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.RestaurantId==restaurantId&&i.IsActive==true).ToListAsync();
        }
    }
    public async Task<List<Product>> GetSellerProductsByRestaurantIdAsync(int restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.RestaurantId==restaurantId).ToListAsync();
        }
    }

    public async Task<List<Product>> GetProductsBySearchNameAsync(string searchName)
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();
        }
    }

    public async Task<int> TotalProdutsNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Products.CountAsync();
        }
    }

    public async Task<List<Product>> Admin_GetProducts30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Products.OrderBy(i=>i.CreatedTime).Take(30).ToListAsync();
        }
    }

    public async Task<List<Product>> GetTotalProductsByRestaurantIdAsync(int restaurantId)
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.RestaurantId==restaurantId).ToListAsync();
        }
    }

    public async Task<List<Product>> Admin_GetProductsBySearchAsync(string search)
    {
        using(var context = new DataContext())
        {
            int productId=-1;
            try{
                productId=Convert.ToInt32(search);
            }
            catch(Exception err)
            {
                productId=-1;
            }
            return await context.Products.Where(i=>i.Id==productId||i.Name.ToLower().Contains(search.ToLower())).ToListAsync();
        }
    }

    public async Task Admin_UpdateCategoryIdNullAsync(int categoryId)
    {
        using(var context = new DataContext())
        {
            var products = await context.Products.Where(i=>i.CategoryId==categoryId).ToListAsync();
            foreach(var p in products)
            {
                p.CategoryId=-1;
                try{
                    context.Update(p);
                    await context.SaveChangesAsync();
                }
                catch(Exception err){
                    Console.WriteLine(err);
                }
            }
        }
    }

    public async Task<List<Product>> Admin_GetNotApprovedProducts30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.IsApproved==false).Take(30).ToListAsync();
            
        }
    }

    public async Task<List<Product>> Admin_GetNotApprovedProductsBySearchAsync(string search)
    {
        using(var context = new DataContext())
        {
            int productId=-1;
            try{
                productId=Convert.ToInt32(search);
            }
            catch(Exception err)
            {
                productId=-1;
            }
            return await context.Products.Where(i=>(i.Id==productId||i.Name.ToLower().Contains(search.ToLower()))&&i.IsApproved==false).Take(30).ToListAsync();
        }
    }

    public async Task<int> Admin_GetNotApprovedProductsNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Products.Where(i=>i.IsApproved==false).CountAsync();
            
        }
    }
}
