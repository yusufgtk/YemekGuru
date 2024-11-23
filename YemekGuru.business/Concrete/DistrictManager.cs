using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class DistrictManager : IDistrictService
{
    private readonly IDistrictRepository _districtRepository;
    public DistrictManager(IDistrictRepository districtRepository)
    {
        _districtRepository=districtRepository;
    }
    public async Task<bool> CreateAsync(District entity)
    {
        return await _districtRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(District entity)
    {
        return await _districtRepository.DeleteAsync(entity);
    }

    public async Task<List<District>> GetAllAsync()
    {
        return await _districtRepository.GetAllAsync();
    }

    public async Task<List<District>> GetDistrictsAsync()
    {
        return await _districtRepository.GetDistrictsAsync();
    }

    public async Task<List<District>> GetDistrictsByCityIdAsync(int cityId)
    {
        return await _districtRepository.GetDistrictsByCityIdAsync(cityId);
    }

    public async Task<District> GetDistrictByIdAsync(int? id)
    {
        return await _districtRepository.GetDistrictByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(District entity)
    {
        return await _districtRepository.UpdateAsync(entity);
    }
}
