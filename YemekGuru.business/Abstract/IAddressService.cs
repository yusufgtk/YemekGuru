using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface IAddressService
{
    public Task<bool> CreateAsync(Address entity);
    public Task<bool> DeleteAsync(Address entity);
    public Task<bool> UpdateAsync(Address entity);
    public Task<List<Address>> GetAllAsync();
    public Task<Address> GetAddressByIdAsync(int? id);
    public Task<List<Address>> GetAddressesAsync();
    public Task<List<Address>> GetAddressesByUserIdAsync(string userId);
}