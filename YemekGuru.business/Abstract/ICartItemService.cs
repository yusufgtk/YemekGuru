using YemekGuru.entity;

namespace YemekGuru.business.Abstract;

public interface ICartItemService
{
    public Task<bool> CreateAsync(CartItem entity);
    public Task<bool> DeleteAsync(CartItem entity);
    public Task<bool> UpdateAsync(CartItem entity);
    public Task<List<CartItem>> GetAllAsync();
    public Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
    public Task<List<CartItem>> GetCartItemsByProductIdAsync(int productId);
    public Task<bool> IsThereAnyProductAsync(int productId);
    public Task<int> HaveCartItemUsersNumber();
}