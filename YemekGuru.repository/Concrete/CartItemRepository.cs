using Microsoft.EntityFrameworkCore;
using YemekGuru.entity;
using YemekGuru.repository.Abstract;

namespace YemekGuru.repository.Concrete;

public class CartItemRepository : Repository<CartItem, DataContext>, ICartItemRepository
{
    public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
    {
        using(var context = new DataContext())
        {
            return await context.CartItems
                        .Include(i => i.Product)
                        .Where(i => i.CartId == cartId)
                        .ToListAsync();
        }
    }

    public async Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId)
    {
        using(var context = new DataContext())
        {
            return await context.CartItems.Where(i=>i.ProductId==productId).ToListAsync();
        }
    }

    public async Task<int> HaveCartItemUsersNumber()
    {
        using(var context = new DataContext())
        {
            return await context.CartItems.GroupBy(r => r.CartId).CountAsync();
        }
    }

    public async Task<bool> IsThereAnyProductAsync(int productId)
    {
        using(var context = new DataContext())
        {
            var cartItem = await context.CartItems.FirstOrDefaultAsync(i=>i.ProductId==productId);
            if(cartItem!=null)
            {
                return true;
            }
            return false;
        }
    }
}
