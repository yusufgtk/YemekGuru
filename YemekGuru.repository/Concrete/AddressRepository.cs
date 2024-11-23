using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class AddressRepository : Repository<Address, DataContext>, IAddressRepository
{
    public async Task<List<Address>> GetAddressesAsync()
    {
        using(var context = new DataContext())
        {
            return await context.Addresses.ToListAsync();
        }
    }

    public async Task<Address> GetAddressByIdAsync(int? id)
    {
        using(var context = new DataContext())
        {
            return await context.Addresses.FirstOrDefaultAsync(i=>i.Id==id);
        }
    }

    public async Task<List<Address>> GetAddressesByUserIdAsync(string userId)
    {
        using(var context = new DataContext())
        {
            return await context.Addresses.Where(i=>i.UserId==userId).ToListAsync();
        }
    }
}
