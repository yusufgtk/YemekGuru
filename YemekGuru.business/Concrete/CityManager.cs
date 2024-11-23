using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class CityManager : ICityService
{
    private readonly ICityRepository _cityRepository;
    public CityManager(ICityRepository cityRepository)
    {
        _cityRepository=cityRepository;
    }
    public async Task<bool> CreateAsync(City entity)
    {
        return await _cityRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(City entity)
    {
        return await _cityRepository.DeleteAsync(entity);
    }

    public async Task<List<City>> GetAllAsync()
    {
        return await _cityRepository.GetAllAsync();
    }

    public async Task<List<City>> GetCitiesAsync()
    {
        return await _cityRepository.GetCitiesAsync();
    }

    public async Task<City> GetCityByIdAsync(int? id)
    {
        return await _cityRepository.GetCityByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(City entity)
    {
        return await _cityRepository.UpdateAsync(entity);
    }
}
