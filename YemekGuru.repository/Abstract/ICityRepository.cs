using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface ICityRepository:IRepository<City>
{
    public Task<City> GetCityByIdAsync(int? id);

    public Task<List<City>> GetCitiesAsync();

}