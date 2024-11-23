using YemekGuru.business.Abstract;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.business.Concrete;

public class AddressManager : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    public AddressManager(IAddressRepository addressRepository)
    {
        _addressRepository=addressRepository;
    }
    public async Task<bool> CreateAsync(Address entity)
    {
        return await _addressRepository.CreateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Address entity)
    {
        return await _addressRepository.DeleteAsync(entity);
    }

    public async Task<List<Address>> GetAddressesAsync()
    {
        return await _addressRepository.GetAddressesAsync();
    }

    public async Task<Address> GetAddressByIdAsync(int? id)
    {
        return await _addressRepository.GetAddressByIdAsync(id);
    }

    public async Task<List<Address>> GetAddressesByUserIdAsync(string userId)
    {
        return await _addressRepository.GetAddressesByUserIdAsync(userId);
    }

    public async Task<List<Address>> GetAllAsync()
    {
        return await _addressRepository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Address entity)
    {
        return await _addressRepository.UpdateAsync(entity);
    }
}
