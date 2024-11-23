using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IDistrictService
{
    public Task<bool> CreateAsync(District entity);
    public Task<bool> DeleteAsync(District entity);
    public Task<bool> UpdateAsync(District entity);
    public Task<List<District>> GetAllAsync();
    public Task<District> GetDistrictByIdAsync(int? id);
    public Task<List<District>> GetDistrictsAsync();
    public Task<List<District>> GetDistrictsByCityIdAsync(int cityId);
}