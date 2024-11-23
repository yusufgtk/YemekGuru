using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class CityRepository : Repository<City, DataContext>, ICityRepository
{
    public async Task<List<City>> GetCitiesAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Cities.Where(i=>i.IsActive==true).ToListAsync();
        }
    }

    public async Task<City> GetCityByIdAsync(int? id)
    {
        using(var context = new DataContext())
        {
            return await context.Cities.FirstOrDefaultAsync(i=>i.Id==id);
        }
        
    }
}
