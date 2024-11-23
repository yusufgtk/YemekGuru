using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IDistrictRepository:IRepository<District>
{
    public Task<District> GetDistrictByIdAsync(int? id);
    public Task<List<District>> GetDistrictsAsync();
    public Task<List<District>> GetDistrictsByCityIdAsync(int cityId);
    
}