using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface ICityService
{
    public Task<bool> CreateAsync(City entity);
    public Task<bool> DeleteAsync(City entity);
    public Task<bool> UpdateAsync(City entity);
    public Task<List<City>> GetAllAsync();
    public Task<City> GetCityByIdAsync(int? id);
    public Task<List<City>> GetCitiesAsync();
}