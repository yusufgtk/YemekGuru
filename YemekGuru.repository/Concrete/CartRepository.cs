using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class CartRepository : Repository<Cart, DataContext>, ICartRepository
{
    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        using(var context = new DataContext())
        {
            return await context.Carts.FirstOrDefaultAsync(i=>i.UserId==userId);
        }
    }
}
