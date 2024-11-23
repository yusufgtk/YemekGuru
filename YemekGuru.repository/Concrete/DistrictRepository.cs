using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class DistrictRepository : Repository<District, DataContext>, IDistrictRepository
{
    public async Task<List<District>> GetDistrictsAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Districts.Where(i=>i.IsActive==true).ToListAsync();
        }
    }

    public async Task<List<District>> GetDistrictsByCityIdAsync(int cityId)
    {
        using(var context = new DataContext())
        {
            return await context.Districts.Where(i=>i.CityId==cityId&&i.IsActive==true).ToListAsync();
        }
    }

    public async Task<District> GetDistrictByIdAsync(int? id)
    {
        using(var context = new DataContext())
        {
            return await context.Districts.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }
}
