using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class RestaurantRepository : Repository<Restaurant, DataContext>, IRestaurantRepository
{
    public async Task<List<Restaurant>> Admin_GetNotApprovedRestaurants30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>i.IsApproved==false).Take(30).ToListAsync();
        }
    }

    public async Task<List<Restaurant>> Admin_GetNotApprovedRestaurantsBySearchAsync(string search)
    {
        using(var context = new DataContext())
        {
            var restaurantId=-1;
            try
            {
                restaurantId=Convert.ToInt32(search);
            }
            catch(Exception err)
            {
                restaurantId=-1;
            }
            return await context.Restaurants.Where(i=>(i.Id==restaurantId||i.Name.ToLower().Contains(search.ToLower().Trim()))&&i.IsApproved==false).Take(30).ToListAsync();
        }
    }

    public async Task<List<Restaurant>> Admin_GetRestaurants30Async()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Take(30).ToListAsync();
        }
    }

    public async Task<List<Restaurant>> Admin_GetRestaurantsBySearchAsync(string search)
    {
        using(var context = new DataContext())
        {
            int restaurantId = -1;
            try{
                restaurantId=Convert.ToInt32(search);
            }
            catch(Exception err)
            {
                restaurantId = -1;
                Console.WriteLine(err);
            }
            return await context.Restaurants.Where(i=>i.Id==restaurantId||i.Name.ToLower().Contains(search.ToLower())).ToListAsync();
        }
    }

    public async Task<int> CloseRestaurantsNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>i.IsActive==false).CountAsync();
        }
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(int id)
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<Restaurant> GetRestaurantByUserIdAsync(string userId)
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.FirstOrDefaultAsync(i=>i.UserId==userId);
        }
    }

    public async Task<List<Restaurant>> GetRestaurantsByLocationAsync(string country, int? cityId, int? districtId)
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>(i.Country==country)&&(i.CityId==cityId)&&(i.DistrictId==districtId)&&(i.IsApproved==true)).ToListAsync();
        }
    }

    public async Task<List<Restaurant>> GetRestaurantsByLocationSeachNameAsync(string? searchName, string country, int? cityId, int? districtId)
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>(i.Name.ToLower().Contains(searchName.ToLower().Trim()))&&(i.Country==country)&&(i.CityId==cityId)&&(i.DistrictId==districtId)&&(i.IsApproved==true)).ToListAsync();
        }
    }

    public async Task<int> NotApprovedRestaurantNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>i.IsApproved==false).CountAsync();
        }
    }

    public async Task<int> OpenRestaurantsNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.Where(i=>i.IsActive==true).CountAsync();
        }
    }

    public async Task<int> TotalRestaurantsNumberAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Restaurants.CountAsync();
        }
    }
}
