using YemekGuru.entity;

namespace YemekGuru.repository.Abstract;

public interface IAddressRepository:IRepository<Address>
{
    public Task<Address> GetAddressByIdAsync(int? id);
    
    public Task<List<Address>> GetAddressesAsync();
    public Task<List<Address>> GetAddressesByUserIdAsync(string userId);
}